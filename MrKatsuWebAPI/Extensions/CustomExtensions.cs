using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MrKatsuWebAPI.Application.Mail;
using MrKatsuWebAPI.Application.Redis;
using MrKatsuWebAPI.Application.Tokens;
using MrKatsuWebAPI.DataAccess;
using MrKatsuWebAPI.DataAccess.Entities;
using MrKatsuWebAPI.DTO.Authorize;
using MrKatsuWebAPI.DTO.Settings;
using MrKatsuWebAPI.Helper;
using MySqlConnector;
using StackExchange.Redis;
using System.Data;
using System.IO.Compression;
using System.Text;

namespace MrKatsuWebAPI.Extensions
{
    public static class CustomExtensions
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString(SystemConstant.DB_CONNECTION_STRING);

            services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(connectionString)));

            services.AddSingleton<IDbConnection>(sp => new MySqlConnection(connectionString));
            return services;
        }
        public static IServiceCollection InjectDbContextPool(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString(SystemConstant.DB_CONNECTION_STRING);

            // Đăng ký DbContext với Pooling mặc định của MySQL
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseMySql(
                    connectionString,
                    Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(connectionString),
                    mySqlOptions =>
                    {
                        mySqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null);
                    });
            });

            return services;
        }
        public static IServiceCollection AddSmtpConfig(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MailSetting>(config.GetSection(SystemConstant.SMTP_SETTINGS));
            services.AddSingleton<IMailService, MailService>();
            return services;
        }
        public static IServiceCollection ConfigureRedis(this IServiceCollection services, IConfiguration config)
        {
            var connectionStringRedis = config.GetConnectionString(SystemConstant.REDIS_CONNECTION_STRING);
            if (string.IsNullOrEmpty(connectionStringRedis))
                throw new ArgumentNullException(nameof(connectionStringRedis), "Redis connection string is missing.");
            services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(connectionStringRedis));

            services.AddScoped<IRedisService, RedisService>();

            return services;
        }
        public static IApplicationBuilder ConfigureCORS(this IApplicationBuilder app)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            var allowedOrigins = new string[]
            {
                "http://localhost:4200",
                "https://gavlwebsite-hbc6afe9a9caggaq.southeastasia-01.azurewebsites.net",
                "https://gavl.io.vn",
                "https://www.gavl.io.vn",
                "https://forum.gavl.io.vn",
                "https://www.forum.gavl.io.vn"
            };
            app.UseCors(options =>
                options.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins(allowedOrigins));
            return app;
        }
        //Identity Configuration
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(15);
            });

            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();

            return services;
        }
        // JWT Configuration
        public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtSettings>(config.GetSection(SystemConstant.JWT_SETTING));
            services.AddScoped<TokenService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["JwtSettings:Issuer"],
                    ValidAudience = config["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Secret"]!))
                };
            });

            return services;
        }
        // Configure the HTTP request pipeline
        public static IApplicationBuilder AddIdentityAuthMiddlewares(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
        //Swagger Configure
        public static IServiceCollection AddSwaggerExplorer(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger MrKatsuWeb Solution", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
            return services;
        }
        public static IApplicationBuilder ConfigureSwaggerExplorer(this IApplicationBuilder app)
        {
            var ev = app.ApplicationServices.GetRequiredService<IHostEnvironment>();
            if (ev.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            return app;
        }
        public static IServiceCollection ConfigureServicePayload(this IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = int.MaxValue;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true; // Cho phép nén trên HTTPS
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
            });
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest; // Có thể là Optimal, Fastest, NoCompression
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            return services;
        }
        public static IApplicationBuilder ConfigureAppPayLoad(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                if (context.Request.Headers["Content-Encoding"] == "gzip")
                {
                    context.Request.Body = new GZipStream(context.Request.Body, CompressionMode.Decompress);
                }
                await next();
            });

            app.UseResponseCompression();
            return app;
        }
    }
}
