using MassTransit;
using MassTransit.RabbitMqTransport;
using MassTransit.RabbitMqTransport.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TravelAgency.API.Extensions
{
    public static class MasstransitConfigurationExtension
    {
        public static void ConfigureMasstransit(this IServiceCollection services)
        {
            services.AddMassTransit(cfg =>
            {
                cfg.UsingRabbitMq(Configure);
            });

            services.AddMassTransitHostedService();
        }

        private static void Configure(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator configurator)
        {
            configurator.ConfigureEndpoints(context);
        }
    }
}
