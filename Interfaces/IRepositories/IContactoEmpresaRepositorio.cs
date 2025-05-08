using ApiPortafolio.Entities;

namespace ApiPortafolio.Interfaces.IRepositories
{
    public interface IContactoEmpresaRepositorio
    {
        Task AgregarContactoAsync(EmpresaContacto empresa);
    }
}
