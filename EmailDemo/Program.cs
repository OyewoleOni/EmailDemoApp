using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25
               // DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
               // PickupDirectoryLocation = @"C:\oni\Demos"
            });

            StringBuilder template = new();
            template.AppendLine("Dear @Model.FirstName,");
            template.AppendLine("<p>Thnaks for purchasing @Model.ProductName. We hope you enjoyed it. </p>");
            template.AppendLine("- The OniCo Team");

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();
            var email = await Email
                    .From("oni@onico.com")
                    .To("test@test.com", "ayo")
                    .Subject("Thanks")
                    .UsingTemplate(template.ToString(), new { FirstName = "Oni", ProductName = "Boli-wrapped Boli"})
                    //.Body("Thanks for buying our product")
                    .SendAsync();
        }
    }
}
