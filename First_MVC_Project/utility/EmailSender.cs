using Microsoft.AspNetCore.Identity.UI.Services;

namespace First_MVC_Project.utility
{
    public class EmailSender : IEmailSender
    {
        //public Task SendEmailAsync(string email, string subject, string htmlMessage)
        //{
        //    var smtpClient = new SmtpClient("ahmed0a41468@gmail.com")
        //    {
        //        Port = 587,
        //        Credentials = new NetworkCredential("ahmed0a41468@gmail.com", "gjywhcplxfhouzga"),
        //        EnableSsl = true,
        //    };
        //    return smtpClient.SendMailAsync("ahmed0a41468@gmail.com", email, subject, htmlMessage);
        //}
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
