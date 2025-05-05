using MrKatsuWebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InjectDbContextPool(builder.Configuration)
                .ConfigureRedis(builder.Configuration)
                .ConfigureIdentity()
                .ConfigureJwt(builder.Configuration)
                .ConfigureServicePayload()
                .AddSwaggerExplorer()
                .AddSmtpConfig(builder.Configuration)
                .AddDIService();  
builder.Services.AddControllers();
//Thêm log
builder.Logging.AddConsole();

var app = builder.Build();

app.ConfigureCORS()
    .ConfigureAppPayLoad()
   .ConfigureSwaggerExplorer()
   .AddIdentityAuthMiddlewares();

app.MapControllers();

app.Run();
