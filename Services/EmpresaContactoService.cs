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

        public EmpresaContactoService(IContactoEmpresaRepositorio repo, IEmailService emailService)
        {
            this.repo = repo;
            this.emailService = emailService;
        }

        public async Task ProcesarContactoAsync(EmpresaContactoDto dto)
        {
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
