using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MrKatsuWebAPI.Application.Tokens;
using MrKatsuWebAPI.DataAccess;
using MrKatsuWebAPI.DataAccess.Configurations;
using MrKatsuWebAPI.DataAccess.Entities;
using MrKatsuWebAPI.DTO.Settings;
using MrKatsuWebAPI.Helper;
using System.Text;

namespace MrKatsuWebAPI.Extensions
{
    public static class CustomExtensions
    {
        public static IApplicationBuilder ConfigureCORS(this IApplicationBuilder app)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            var allowedOrigins = new string[]
            {
                "http://localhost:4200",
                "https://gatapchoi.id.vn",
                "https://www.gatapchoi.id.vn"
            };
            app.UseCors(options =>
                options.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins(allowedOrigins));
            return app;
        }
        public static IServiceCollection ConfigureMongoDb(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MongoDbSettings>(config.GetSection(SystemConstant.MONGODB_SETTING));
            services.AddSingleton<MongoDbContext>();
            return services;
        }
        //Identity Configuration
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddUserStore<MongoIdentityUserStore>()
            .AddRoleStore<MongoIdentityRoleStore>()
            .AddDefaultTokenProviders();
            return services;
        }
        // JWT Configuration
        public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtSettings>(config.GetSection(SystemConstant.JWT_SETTING)); 
            services.AddSingleton<TokenService>();
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
    }
}
