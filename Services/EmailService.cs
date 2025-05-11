using ApiPortafolio.DTOs;
using ApiPortafolio.Interfaces.IServices;
using System.Net.Mail;
using System.Net;

namespace ApiPortafolio.Services
{
    public class EmailService: IEmailService
    {
        private readonly IConfiguration config;
        private readonly ILogger<EmailService> logger;

        public EmailService(IConfiguration config, ILogger<EmailService> logger)
        {
            this.config = config;
            this.logger = logger;
        }

        public async Task EnviarCorreoAsync(EmpresaContactoDto dto)
        {
            logger.LogInformation("Intentando enviar correo a: {Correo}", dto.Correo);
            if (string.IsNullOrWhiteSpace(dto.Correo))
            {
                logger.LogError("El correo es nulo o vacío. No se puede enviar el correo.");
                throw new ArgumentException("El correo no puede estar vacío.");
            }

            // Preferencia por appsettings.json, fallback a variables de entorno
            string smtpUser = config["Smtp:User"] ?? Environment.GetEnvironmentVariable("SMTP__User");
            string smtpPass = config["Smtp:Pass"] ?? Environment.GetEnvironmentVariable("SMTP__Pass");
            string smtpHost = config["Smtp:Host"] ?? Environment.GetEnvironmentVariable("SMTP__Host");
            string smtpPort = config["Smtp:Port"] ?? Environment.GetEnvironmentVariable("SMTP__Port");

            // Validación
            if (string.IsNullOrEmpty(smtpUser) || string.IsNullOrEmpty(smtpPass) || string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(smtpPort))
            {
                logger.LogError("No se pudo obtener configuración SMTP. Verifica appsettings.json o variables de entorno.");
                throw new InvalidOperationException("Faltan configuraciones SMTP.");
            }

            var mail = new MailMessage
            {
                From = new MailAddress(smtpUser),
                Subject = "Nuevo contacto de empresa",
                Body = $"Empresa: {dto.NombreEmpresa}\n" +
                       $"Contacto: {dto.NombreContacto}\n" +
                       $"Correo: {dto.Correo}\n" +
                       $"Teléfono: {dto.Telefono}\n\nMensaje:\n{dto.Mensaje}",
                IsBodyHtml = false
            };

            mail.To.Add(smtpUser); // destinatario

            using var smtpClient = new SmtpClient(smtpHost, int.Parse(smtpPort))
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mail);
        }
    }
}
