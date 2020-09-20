using TravelAgency.Contracts.Commands.SharedCommandContracts;

namespace TravelAgency.Models
{
    public class VacationExtras : IVacationExtras
    {
        public CarRental CarRental { get; set; }

        ICarRental IVacationExtras.CarRental => this.CarRental;
    }
}
