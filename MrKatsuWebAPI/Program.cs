using MrKatsuWebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureMongoDb(builder.Configuration)
                .ConfigureIdentity()
                .ConfigureJwt(builder.Configuration)
                .AddSwaggerExplorer();  
builder.Services.AddControllers();

var app = builder.Build();

app.ConfigureCORS()
   .ConfigureSwaggerExplorer()
   .AddIdentityAuthMiddlewares();

app.MapControllers();

app.Run();
