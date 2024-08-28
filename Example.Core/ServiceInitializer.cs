using Example.Data;
using Example.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Example.Core
{
	public static partial class ServiceInitializer
	{
		public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
		{
			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddHttpContextAccessor();

			services.AddScoped<ISqlClientConnectionBD, SqlClientConnectionBD>();

			//Services
			services.AddScoped<ICustomerService, CustomerService>();

			return services;
		}
	}
}
