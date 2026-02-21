using Microsoft.AspNetCore.Mvc;
using SMSWebApi.DTO;
using SMSWebApi.Services;

namespace SMSWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmsController : ControllerBase
    {
        private readonly ColaService _colaService;

        public SmsController(ColaService colaService)
        {
            _colaService = colaService;
        }

        [HttpPost("enviar")]
        public IActionResult Send([FromBody] SMSRequest request)
        {
            if (string.IsNullOrEmpty(request.Telefono) || string.IsNullOrEmpty(request.Mensaje))
            {
                return BadRequest(new { error = "El Telefono y el mensaje son requeridos" });
            }

            var messageId = _colaService.Encolar(request.Telefono, request.Mensaje);

            return Ok(new
            {
                success = true,
                messageId = messageId,
                mensaje = "Mensaje encolado.",
                pendientes = _colaService.ContarPendientes()
            });
        }

        [HttpGet("history")]
        public IActionResult History([FromQuery] int limite = 10)
        {
            var historial = _colaService.ObtenerHistorial(limite);
            return Ok(historial);
        }
    }
}
