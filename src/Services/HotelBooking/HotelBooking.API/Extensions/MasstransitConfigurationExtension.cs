using HotelBooking.Components.Consumers;
using MassTransit;
using MassTransit.Definition;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HotelBooking.API.Extensions
{
    public static class MasstransitConfigurationExtension
    {
        public static void ConfigureMasstransit(this IServiceCollection services)
        {
            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumersFromNamespaceContaining<ReserveHotelConsumer>();

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
