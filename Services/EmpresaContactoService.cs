using ApiPortafolio.DTOs;
using ApiPortafolio.Entities;
using ApiPortafolio.Interfaces.IRepositories;
using ApiPortafolio.Interfaces.IServices;

namespace ApiPortafolio.Services
{
    public class EmpresaContactoService
    {
        private readonly IContactoEmpresaRepositorio repo;
        private readonly IEmailService emailService;
        private readonly ILogger<EmpresaContactoService> logger;

        public EmpresaContactoService(IContactoEmpresaRepositorio repo, IEmailService emailService, ILogger<EmpresaContactoService> logger)
        {
            this.repo = repo;
            this.emailService = emailService;
            this.logger = logger;
        }

        public async Task ProcesarContactoAsync(EmpresaContactoDto dto)
        {
            logger.LogInformation("DTO recibido: {@dto}", dto);

            if (string.IsNullOrWhiteSpace(dto.Correo))
            {
                logger.LogWarning("El campo 'Correo' está vacío o nulo.");
            }

            var empresa = new EmpresaContacto
            {
                NombreEmpresa = dto.NombreEmpresa,
                NombreContacto = dto.NombreContacto,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Mensaje = dto.Mensaje,
                FechaRegistro = DateTime.UtcNow
            };

            await repo.AgregarContactoAsync(empresa);
            await emailService.EnviarCorreoAsync(dto);
        }
    }
}
