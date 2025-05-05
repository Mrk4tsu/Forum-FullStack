using MrKatsuWebAPI.Application.Mods;
using MrKatsuWebAPI.Application.Posts;
using MrKatsuWebAPI.Application.Replies;
using MrKatsuWebAPI.Application.Systems;
using MrKatsuWebAPI.Application.Systems.Cloudflares;

namespace MrKatsuWebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDIService(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<ICloudflareTurnstileService, CloudflareTurnstileService>();

            services.AddScoped<IAuthService, AuthServices>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IReplyService, ReplyService>();
            services.AddScoped<IModService, ModService>();
            return services;
        }
    }
}
