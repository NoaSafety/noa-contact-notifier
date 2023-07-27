using Microsoft.Extensions.DependencyInjection;
using Noa.ContactNotifier.Docker.Configuration;
using Noa.ContactNotifier.Docker.Services;
using Noa.PocketUi.Main.Services;

namespace Noa.ContactNotifier.Docker
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = Configuration.GetSection(ServiceConfiguration.ServiceName).Get<ServiceConfiguration>();

            services.Configure<ServiceConfiguration>(Configuration.GetSection(ServiceConfiguration.ServiceName));

            services.AddCorsHandling(configuration);

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.ConfigureNoaServices(configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCorsApplication();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplicationServices.GetService<IMqttService>();
        }
    }
}
