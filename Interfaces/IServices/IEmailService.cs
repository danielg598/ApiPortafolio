using ApiPortafolio.DTOs;

namespace ApiPortafolio.Interfaces.IServices
{
    public interface IEmailService
    {
        Task EnviarCorreoAsync(EmpresaContactoDto dto);
    }
}
