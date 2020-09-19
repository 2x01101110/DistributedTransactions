using MassTransit;
using MassTransit.Definition;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Contracts.CarRenting;
using TravelAgency.Components.Consumers;
using TravelAgency.Components.CourierActivities.RentCarActivity;
using TravelAgency.Components.StateMachines.VacationBooking;
using TravelAgency.Components.StateMachines.VacationBooking.Activities;
using TravelAgency.Contracts.Commands;
using TravelAgency.Contracts.Commands.FulfillVacationBooking;
using TravelAgency.Contracts.Requests.VacationBookingProcessStateRequest;

namespace TravelAgency.API.Extensions
{
    public static class MasstransitConfigurationExtension
    {
        public static void ConfigureMasstransit(this IServiceCollection services)
        {
            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
            services.AddScoped<VacationBookingProcessActivity>();

            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumersFromNamespaceContaining<VacationBookingConsumer>();

                //cfg.AddActivitiesFromNamespaceContaining<VacationBookingProcessActivity>();
                cfg.AddActivitiesFromNamespaceContaining<RentCarActivity>();

                cfg.AddSagaStateMachine<VacationBookingProcessStateMachine, VacationBookingProcessState>()
                    .MongoDbRepository(c =>
                    {
                        c.Connection = "mongodb://localhost:27017";
                        c.DatabaseName = "vacation-state-machine";
                    });

                cfg.UsingRabbitMq(Configure);

                cfg.AddRequestClient<IBookVacation>();
                cfg.AddRequestClient<IVacationBookingProcessStateRequest>();
                cfg.AddRequestClient<IFulfillVacationBooking>();
                cfg.AddRequestClient<IRentCar>();
            });

            services.AddMassTransitHostedService();
        }

        private static void Configure(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator configurator)
        {
            configurator.ConfigureEndpoints(context);
        }
    }
}
