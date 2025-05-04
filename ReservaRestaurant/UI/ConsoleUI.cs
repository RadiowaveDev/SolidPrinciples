using ReservaRestaurant.Models;
using ReservaRestaurant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRestaurant.UI
{
    public class ConsoleUI
    {
        private readonly IReservationService _reservationService;
        private readonly ITableService _tableService;
        private readonly ICustomerService _customerService;

        private bool _isRunning = true;

        public ConsoleUI(IReservationService reservationService, ITableService tableService,
            ICustomerService customerService)
        {
            _reservationService = reservationService;
            _tableService = tableService;
            _customerService = customerService;
        }

        public void Run()
        {
            Console.WriteLine("=== Welcome to our DaftDelicias Reservation System ===");
            while (_isRunning)
            {
                DisplayMenu();

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CreateReservation();
                        break;
                    case "2":
                        ViewReservations();
                        break;
                    case "3":
                        CancelReservation();
                        break;
                    case "4":
                        ViewTables();
                        break;
                    case "5":
                        ManageCustomers();
                        break;
                    case "0":
                        _isRunning = false;
                        Console.WriteLine("Thank you for using the Restaurant Reservation System!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine("\n=== Main Menu ===");
            Console.WriteLine("1. Create Reservation");
            Console.WriteLine("2. View Reservations");
            Console.WriteLine("3. Cancel Reservation");
            Console.WriteLine("4. View Tables");
            Console.WriteLine("5. Manage Customers");
            Console.WriteLine("0. Exit");
            Console.Write("Enter option: ");
        }

        private void CreateReservation()
        {
            Console.Clear();
            Console.WriteLine("=== Create Reservation ===");

            // Get customer information
            Console.Write("Customer name: ");
            var name = Console.ReadLine();

            Console.Write("Phone number: ");
            var phone = Console.ReadLine();

            Console.Write("Email: ");
            var email = Console.ReadLine();

            var customer = _customerService.CreateCustomer(name, phone, email);

            // Get Reserveation Details
            Console.Write("Reservation date (MM/DD/YYYY HH:MM): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            Console.Write("Reservation time (HH:MM): ");
            if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan time))
            {
                Console.WriteLine("Invalid time format.");
                return;
            }

            Console.Write("Party size: ");
            if (!int.TryParse(Console.ReadLine(), out int partySize) || partySize <= 0)
            {
                Console.WriteLine("Invalid party size.");
                return;
            }

            Console.Write("Special requests (optional): ");
            var specialRequests = Console.ReadLine();

            // Find available tables
            var availableTables = _tableService.GetAvailableTables(date, partySize);

            if (availableTables.Count == 0)
            {
                Console.WriteLine("No available tables found.");
                return;
            }

            // Display available tables
            Console.WriteLine("\nAvailable Tables:");
            for (int i = 0; i < availableTables.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableTables[i]}");
            }

            // Select table
            Console.Write("\nSelect table (number): ");
            if (!int.TryParse(Console.ReadLine(), out int tableIndex) ||
                tableIndex < 1 || tableIndex > availableTables.Count)
            {
                Console.WriteLine("Invalid table selection.");
                return;
            }

            var selectedTable = availableTables[tableIndex - 1];

            // Create Reservation
            var reservation = new Reservation()
            {
                CustomerId = customer.Id,
                TableId = selectedTable.Id,
                Date = date,
                Time = time,
                PartySize = partySize,
                SpecialRequest = specialRequests,
                Status = Models.ReservationStatus.Confirmed
            };

            var confirmation = _reservationService.CreateReservation(reservation);

            if (confirmation)
            {
                Console.WriteLine($"Reservation created successfully! Reservation ID: {reservation.Id}");
            }
            else
            {
                Console.WriteLine("Reservation creation failed.");
            }
        }

        private void CancelReservation()
        {
            Console.Clear();
            Console.WriteLine("=== Cancel Reservation ===");

            Console.Write("Enter reservation ID: ");
            if (!int.TryParse(Console.ReadLine(), out int reservationId))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            var reservation = _reservationService.GetReservationById(reservationId);

            if (reservation == null)
            {
                Console.WriteLine("Reservation not found.");
                return;
            }

            var customer = _customerService.GetCustomerById(reservation.CustomerId);

            Console.WriteLine($"\nReservation details:");
            Console.WriteLine($"Customer: {customer.FullName}");
            Console.WriteLine($"Date: {reservation.Date.ToShortDateString()}, Time: {reservation.Time}");
            Console.WriteLine($"Party Size: {reservation.PartySize}");
            Console.WriteLine($"Status: {reservation.Status}");

            Console.Write("\nAre you sure you want to cancel this reservation? (y/n): ");
            var confirm = Console.ReadLine().ToLower();

            if (confirm == "y" || confirm == "yes")
            {
                var result = _reservationService.CancelReservation(reservation);
                if (result)
                {
                    Console.WriteLine($"Reservation cancelled successfully! Reservation ID: {reservation.Id}");
                }
                else
                {
                    Console.WriteLine("Operation cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
        }

        private void ViewReservations()
        {
            Console.Clear();
            Console.WriteLine("=== View Reservations ===");
            Console.WriteLine("1. View by date");
            Console.WriteLine("2. View by customer");
            Console.Write("Enter option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ViewReservationsByDate();
                    break;
                case "2":
                    ViewReservationsByCustomer();
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        private void ViewReservationsByDate()
        {
            Console.Write("Enter date (MM/DD/YYYY): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            var reservations = _reservationService.GetReservationsByDate(date);

            if (reservations.Count == 0)
            {
                Console.WriteLine("No reservations found for the selected date.");
                return;
            }

            Console.WriteLine($"\nReservations for {date.ToShortDateString()}:");
            foreach (var reservation in reservations)
            {
                var customer = _customerService.GetCustomerById(reservation.CustomerId);
                var table = _tableService.GetTableById(reservation.TableId);

                Console.WriteLine($"ID: {reservation.Id}, Customer: {customer.FullName}, " +
                                  $"Time: {reservation.Time}, Table: {table.Number}, " +
                                  $"Party Size: {reservation.PartySize}, Status: {reservation.Status}");
            }
        }

        private void ViewReservationsByCustomer()
        {
            Console.Write("Enter customer email: ");
            var email = Console.ReadLine();

            var customer = _customerService.GetCustomerByEmail(email);

            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            var reservations = _customerService.GetCustomerReservations(customer.Id);

            if (reservations.Count == 0)
            {
                Console.WriteLine("No reservations found for this customer.");
                return;
            }

            Console.WriteLine($"\nReservations for {customer.FullName}:");
            foreach (var reservation in reservations)
            {
                var table = _tableService.GetTableById(reservation.TableId);

                Console.WriteLine($"ID: {reservation.Id}, Date: {reservation.Date.ToShortDateString()}, " +
                                  $"Time: {reservation.Time}, Table: {table.Number}, " +
                                  $"Party Size: {reservation.PartySize}, Status: {reservation.Status}");
            }
        }

        private void ViewTables()
        {
            Console.Clear();
            Console.WriteLine("=== Tables ===");

            var tables = _tableService.GetAllTables();

            foreach (var table in tables)
            {
                Console.WriteLine(table);
            }
        }

        private void ManageCustomers()
        {
            Console.Clear();
            Console.WriteLine("=== Manage Customers ===");
            Console.WriteLine("1. Search customer by email");
            Console.Write("Enter option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    SearchCustomer();
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        private void SearchCustomer()
        {
            Console.Write("Enter customer email: ");
            var email = Console.ReadLine();

            var customer = _customerService.GetCustomerByEmail(email);

            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            Console.WriteLine($"\nCustomer details:");
            Console.WriteLine($"Name: {customer.FullName}");
            Console.WriteLine($"Phone: {customer.PhoneNumber}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"VIP: {customer.isVIP}");

            var reservations = _customerService.GetCustomerReservations(customer.Id);

            if (reservations.Count > 0)
            {
                Console.WriteLine($"\nReservation History:");
                foreach (var reservation in reservations)
                {
                    Console.WriteLine($"ID: {reservation.Id}, Date: {reservation.Date.ToShortDateString()}, " +
                                      $"Time: {reservation.Time}, Status: {reservation.Status}");
                }
            }
            else
            {
                Console.WriteLine("\nNo reservation history found.");
            }
        }
    }
}
