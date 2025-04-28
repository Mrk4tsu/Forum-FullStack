using MrKatsuWebAPI.Application.Comments;
using MrKatsuWebAPI.Application.Systems;
using MrKatsuWebAPI.Application.Topics;

namespace MrKatsuWebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDIService(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthServices>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ITopicService, TopicServices>();
            return services;
        }
    }
}
