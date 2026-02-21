namespace SMSWebApi.Models
{
    public class SMS
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Telefono { get; set; }
        public string Texto { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Pendiente";
        public string MensajeError { get; set; }
        public DateTime? FechaEnvio { get; set; }
    }
}
