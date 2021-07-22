using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace dotnet_web_api_demo.Services
{

    class TemplateData
    {
        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("button_url")] 
        public string ButtonUrl { get; set; }

    }
    public class EmailService
    {
        private readonly string apiKey;
        private readonly string emailFromName;
        private readonly string emailFromEmail;

        public EmailService(IConfiguration config)
        {
            apiKey = config["EmailService:SENDGRID_API_KEY"];
            emailFromName = config["EmailService:SENDGRID_FROM_NAME"];
            emailFromEmail = config["EmailService:SENDGRID_FROM_EMAIL"];
        }

        public void Main()
        {
            Execute().Wait();
        }

        async Task Execute()
        {
            // print to console
            Console.WriteLine("Sending email...");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(emailFromEmail, emailFromName);
            var subject = "Email Confirmation";
            var to = new EmailAddress("aliahnaf327@gmail.com", "Example User");
            var plainTextContent = "Verify your email address to get started with our website.";
            var htmlContent = "<strong>Verify</strong>  your email address to get started with our website.";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var dynamicTemplateData = new TemplateData
            {
                User = "Microsoft",
                ButtonUrl = "http://localhost:5000/api/verify/token",

            };
            msg.SetTemplateId("d-b99af81ce9654555a1397cb750c9ba98");
            msg.SetTemplateData(dynamicTemplateData);

            var response = await client.SendEmailAsync(msg);
        }
    }

}