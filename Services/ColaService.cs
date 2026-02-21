using SMSWebApi.Models;

namespace SMSWebApi.Services
{
    public class ColaService
    {
        private readonly Queue<SMS> _cola = new Queue<SMS>();
        private readonly Dictionary<Guid, SMS> _historial = new Dictionary<Guid, SMS>();
        private readonly object _lock = new object();

        public Guid Encolar(string telefono, string mensaje)
        {
            var msg = new SMS
            {
                Telefono = telefono,
                Texto = mensaje,
                Estado = "Pendiente"
            };

            lock (_lock)
            {
                _cola.Enqueue(msg);
                _historial[msg.Id] = msg;
            }

            Console.WriteLine($"Mensaje encolado: {msg.Id} - {telefono}");
            return msg.Id;
        }

        public SMS Desencolar()
        {
            lock (_lock)
            {
                if (_cola.Count > 0)
                {
                    var msg = _cola.Dequeue();
                    msg.Estado = "Enviando";
                    return msg;
                }
                return null;
            }
        }

        public void MarcarEnviado(Guid id)
        {
            lock (_lock)
            {
                if (_historial.ContainsKey(id))
                {
                    _historial[id].Estado = "Enviado";
                    _historial[id].FechaEnvio = DateTime.Now;
                }
            }
        }

        public void MarcarError(Guid id, string error)
        {
            lock (_lock)
            {
                if (_historial.ContainsKey(id))
                {
                    _historial[id].Estado = "Error";
                    _historial[id].MensajeError = error;
                    _historial[id].FechaEnvio = DateTime.Now;
                }
            }
        }

        public int ContarPendientes()
        {
            lock (_lock)
            {
                return _cola.Count;
            }
        }

        public List<SMS> ObtenerHistorial(int ultimos = 50)
        {
            lock (_lock)
            {
                var todosMensajes = _historial.Values.ToList();

                todosMensajes.Sort((a, b) => b.FechaCreacion.CompareTo(a.FechaCreacion));

                var resultado = new List<SMS>();

                for (int i = 0; i < ultimos && i < todosMensajes.Count; i++)
                {
                    resultado.Add(todosMensajes[i]);
                }

                return resultado;
            }
        }
    }
}
