using ApiPortafolio.DTOs;
using ApiPortafolio.Interfaces.IServices;
using System.Net.Mail;
using System.Net;

namespace ApiPortafolio.Services
{
    public class EmailService: IEmailService
    {
        private readonly IConfiguration config;

        public EmailService(IConfiguration config)
        {
            this.config = config;
        }

        public async Task EnviarCorreoAsync(EmpresaContactoDto dto)
        {
            var smtp = config.GetSection("Smtp");
            var mail = new MailMessage
            {
                From = new MailAddress(smtp["User"]),
                Subject = "Nuevo contacto de empresa",
                Body = $"Empresa: {dto.NombreEmpresa}\n" +
                       $"Contacto: {dto.NombreContacto}\n" +
                       $"Correo: {dto.Correo}\n" +
                       $"Teléfono: {dto.Telefono}\n\nMensaje:\n{dto.Mensaje}",
                IsBodyHtml = false
            };

            mail.To.Add(smtp["User"]); // correo destinatario

            using var smtpClient = new SmtpClient(smtp["Host"], int.Parse(smtp["Port"]))
            {
                Credentials = new NetworkCredential(smtp["User"], smtp["Pass"]),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mail);
        }
    }
}
