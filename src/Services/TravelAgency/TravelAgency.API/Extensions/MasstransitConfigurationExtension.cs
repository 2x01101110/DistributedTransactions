using MassTransit;
using MassTransit.Definition;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TravelAgency.Components.Consumers;
using TravelAgency.Components.StateMachines.VacationBooking;
using TravelAgency.Contracts.Commands;
using TravelAgency.Contracts.Requests.VacationBookingProcessStateRequest;

namespace TravelAgency.API.Extensions
{
    public static class MasstransitConfigurationExtension
    {
        public static void ConfigureMasstransit(this IServiceCollection services)
        {
            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumersFromNamespaceContaining<BookVacationConsumer>();

                cfg.AddSagaStateMachine<VacationBookingProcessStateMachine, VacationBookingProcessState>()
                    .MongoDbRepository(c =>
                    {
                        c.Connection = "mongodb://localhost:27017";
                        c.DatabaseName = "vacation-state-machine";
                    });

                cfg.UsingRabbitMq(Configure);

                cfg.AddRequestClient<IBookVacation>();
                cfg.AddRequestClient<IVacationBookingProcessStateRequest>();
            });

            services.AddMassTransitHostedService();
        }

        private static void Configure(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator configurator)
        {
            configurator.ConfigureEndpoints(context);
        }
    }
}
