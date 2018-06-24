using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Middleware.Storage;

namespace Middleware
{
	public static class MiddlewareExtensions
	{
		public static void ConfigureRecorder(this IServiceCollection services, IConfigurationRoot configuration)
		{
			services.Configure<RequestStore.Options>(configuration.GetSection("RequestStore"));
			services.Configure<Recorder.Options>(configuration.GetSection("Recorder"));
			services.AddScoped<IRequestStore, RequestStore>();
			services.AddScoped<RecorderMiddleware>();
			services.AddScoped<Recorder>();
		}

		public static void ConfigurePlayer(this IServiceCollection services, IConfigurationRoot configuration)
		{
			services.Configure<ResponseStore.Options>(configuration.GetSection("ResponseStore"));
			services.AddScoped<IResponseStore, ResponseStore>();
			services.AddScoped<PlayerMiddleware>();
			services.AddScoped<Player>();
		}

		public static IApplicationBuilder UseRecorder(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<RecorderMiddleware>();
		}

		public static IApplicationBuilder UsePlayer(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<PlayerMiddleware>();
		}
	}
}