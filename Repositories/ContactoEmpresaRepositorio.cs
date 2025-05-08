using ApiPortafolio.Data;
using ApiPortafolio.Entities;
using ApiPortafolio.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ApiPortafolio.Repositories
{
    public class ContactoEmpresaRepositorio: IContactoEmpresaRepositorio
    {
        private readonly AppDbContext context;

        public ContactoEmpresaRepositorio( AppDbContext context)
        {
            this.context = context;
        }

        public async Task AgregarContactoAsync(EmpresaContacto empresa)
        {
            context.EmpresaContacto.Add(empresa);
            await context.SaveChangesAsync();
        }
    }
}
