using ApiPortafolio.DTOs;
using ApiPortafolio.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiPortafolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactoController: ControllerBase
    {
        private readonly EmpresaContactoService service;

        public ContactoController(EmpresaContactoService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmpresaContactoDto dto)
        {
            await service.ProcesarContactoAsync(dto);
            return Ok(new { mensaje = "Datos registrados y correo enviado." });
        }
    }
}
