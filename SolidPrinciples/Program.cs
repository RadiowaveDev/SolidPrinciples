namespace SolidPrinciples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Crear servicio de notificación con diferentes canales de mensaje
            var servicioNotificacion = new ServicioNotificacion(new List<ICanalMensaje>
        {
            new CanalEmail(),
            new CanalSMS(),
            new CanalNotificacionPush()
        });

            // Enviar una notificación
            Console.WriteLine("Enviando notificación a través de todos los canales disponibles...");
            servicioNotificacion.EnviarNotificacion(new Mensaje
            {
                Titulo = "Demostración de Principios SOLID",
                Contenido = "¡Esta es una demostración de los principios SOLID en acción!"
            });

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }

        // Modelo del mensaje
        public class Mensaje
        {
            public string Titulo { get; set; }
            public string Contenido { get; set; }
        }

        // Principio de Responsabilidad Unica: interfaz que sirve para canales de mensajes
        public interface ICanalMensaje
        {
            void Enviar(Mensaje mensaje);
        }

        // Principio Abierto/Cerrado: Implementaciones concretas de canales de mensaje
        public class CanalEmail : ICanalMensaje
        {
            public void Enviar(Mensaje mensaje)
            {
                Console.WriteLine($"Enviando email: {mensaje.Titulo} - {mensaje.Contenido}");
            }
        }

        public class CanalSMS : ICanalMensaje
        {
            public void Enviar(Mensaje mensaje)
            {
                Console.WriteLine($"Enviando SMS: {mensaje.Titulo} - {mensaje.Contenido}");
            }
        }

        public class CanalNotificacionPush : ICanalMensaje
        {
            public void Enviar(Mensaje mensaje)
            {
                Console.WriteLine($"Enviando notificacion push: {mensaje.Titulo} - {mensaje.Contenido}");
            }
        }

        // Principio de Inversion de Dependencias: ServicioNotificacion depende de abstracciones
        public class ServicioNotificacion
        {
            private readonly IEnumerable<ICanalMensaje> _canales;

            public ServicioNotificacion(IEnumerable<ICanalMensaje> canales)
            {
                _canales = canales ?? throw new ArgumentNullException(nameof(canales));
            }

            // enviar notificacion a traves de todos los canales
            public void EnviarNotificacion(Mensaje mensaje)
            {
                foreach (var canal in _canales)
                {
                    canal.Enviar(mensaje);
                }
            }
        }

        // Principio de Segregacion de Interfaces
        public interface IAlmacenamientoMensaje
        {
            void GuardarMensaje(Mensaje mensaje);
        }

        public interface IRecuperacionMensaje
        {
            Mensaje ObtenerMensajePorId(int id);
            List<Mensaje> ObtenerMensajes(int id);
        }

        public class RepositorioMensajes : IAlmacenamientoMensaje, IRecuperacionMensaje
        {

            private readonly List<Mensaje> _listaMensajes = new List<Mensaje>();

            public void GuardarMensaje(Mensaje mensaje)
            {
                _listaMensajes.Add(mensaje);
            }

            public Mensaje ObtenerMensajePorId(int id)
            {
                return id < _listaMensajes.Count ? _listaMensajes[id] : null;
            }

            public List<Mensaje> ObtenerMensajes(int id)
            {
                return _listaMensajes;
            }
        }

        // Principio de Susticion de Liskov: Los subtipos pueden sustituir a sus tipos base sin alterar la corrección del programa
        public abstract class NotificadorBase
        {
            public virtual void Notificar(Mensaje mensaje)
            {
                Console.WriteLine($"Notificacion base: {mensaje.Titulo}");
            }
        }

        public class NotificadorEstandar : NotificadorBase
        {
            public override void Notificar(Mensaje mensaje)
            {
                Console.WriteLine($"Notificacion estandar: {mensaje.Titulo} - {mensaje.Contenido}");
            }
        }

        public class NotificacionPrioritario : NotificadorBase
        {
            public override void Notificar(Mensaje mensaje)
            {
                Console.WriteLine($"NOTIFICACION PRIORITARIA: {mensaje.Titulo.ToUpper()} - {mensaje.Contenido.ToUpper()}");
            }
        }
    }
}