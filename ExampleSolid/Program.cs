namespace ExampleSolid
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order { Amount = 150.00m };

            IPaymentMethod payment = new CreditCardPayment(); 
            INotificationService notification = new EmailNotification(); 

            CheckoutService checkout = new CheckoutService(payment, notification);
            checkout.Checkout(order);
        }

        /* Principio S: Single Responsability 
         ProcessPayment -> Solo se encarga de pagos
         NotificationService -> Solo se encarga de enviar notificacion de pago
         */
        public class Order
        {
            public decimal Amount { get; set; }
        }

        public class PaymentProcessor
        {
            public string ProcessPayment(Order order, IPaymentMethod method)
            {
                return method.Pay(order.Amount);
            }
        }
        public class NotificationService
        {
            public void SendPaymentConfirmation(string message)
            {
                Console.WriteLine("Enviando notificación: " + message);
            }
        }

        /*Principio O: Open/Close
         CreditCardPayment -> Hace uso de método Pay, ampliando sus aplicaciones
         YapePayment -> Hace uso de método Pay, sin modificar el PaymentProcessor
         */
        public interface IPaymentMethod
        {
            string Pay(decimal amount);
        }

        public class CreditCardPayment : IPaymentMethod
        {
            public string Pay(decimal amount)
            {
                return $"Pago {amount} usando Tarjeta de crédito.";
            }
        }
        public class YapePayment : IPaymentMethod
        {
            public string Pay(decimal amount)
            {
                return $"Pago {amount} usando Yape.";
            }
        }

        /*Principio L: Liskov Sustitution
         PlinPayment -> Clases hijas de IPaymentMethod son intercambiables sin romper el programa
         */
        public class PlinPayment : IPaymentMethod
        {
            public string Pay(decimal amount)
            {
                return $"Pago {amount} usando Plin";
            }
        }

        /*Principio I: Interface Segregation
         La clase solo implementa las interfaces que se utilizaran 
         */
        public interface INotificationService
        {
            void SendPaymentConfirmation(string message);
        }
        public interface ILoggable
        {
            void Log(string message);
        }

        public class EmailNotification : INotificationService, ILoggable
        {
            public void Log(string message)
            {
                Console.WriteLine($"Notificación por Email registrada: {message}");
            }

            public void SendPaymentConfirmation(string message)
            {
                Console.WriteLine("Email enviado: " + message);
            }
        }

        public class SmsNotification : INotificationService
        {
            public void SendPaymentConfirmation(string message)
            {
                Console.WriteLine("SMS enviado: " + message);
            }
        }

        /*Principio D: Dependency Inversion
         El módulo de alto nivel (CheckoutService) depende de las abstracciones(interfaces), 
         no de las implementaciones PaymentMethod y NotificationService
         */
        public class CheckoutService
        {
            private readonly IPaymentMethod _paymentMethod;
            private readonly INotificationService _notification;

            public CheckoutService(IPaymentMethod paymentMethod, INotificationService notification)
            {
                _paymentMethod = paymentMethod;
                _notification = notification;
            }

            public void Checkout(Order order)
            {
                string result = _paymentMethod.Pay(order.Amount);
                _notification.SendPaymentConfirmation(result);
            }
        }
    }
}
