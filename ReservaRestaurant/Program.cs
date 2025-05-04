using ReservaRestaurant.Repositories;
using ReservaRestaurant.Services;
using ReservaRestaurant.UI;

namespace ReservaRestaurant
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Inicializar el sistema
            // Las interfaces no tienen la capacidad de crear instancias
            var reservationService = new ReservationService(new ReservationRepository());
            var tableService = new TableService(new TableRepository());
            var customerService = new CustomerService(new CustomerRepository(), new ReservationRepository());

            // Inicializar nuestra UI (Consola)
            var ui = new ConsoleUI(reservationService, tableService, customerService);

            // Inicializar nuestra aplicacion
            ui.Run();
        }
    }
}
