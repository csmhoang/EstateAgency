using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Infrastructure;

public class EmailSender : IEmailSender
{
    #region Declaration
    private readonly MailJetOptions _mailJetOptions;
    #endregion

    #region Property
    #endregion

    #region Constructor
    public EmailSender(IConfiguration configuration)
    {
        _mailJetOptions = configuration.GetSection("MailJet").Get<MailJetOptions>();
    }
    #endregion

    #region Method
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var client = new MailjetClient(_mailJetOptions.ApiKey, _mailJetOptions.SecretKey);

        var request = new MailjetRequest
        {
            Resource = Send.Resource,
        }
        .Property(Send.FromEmail, "mhoangkt8@gmail.com")
        .Property(Send.FromName, "Minh Hoàng")
        .Property(Send.Subject, subject)
        .Property(Send.HtmlPart, htmlMessage)
        .Property(Send.Recipients, new JArray {
            new JObject {
                {"Email", email}
            }
        });

        var response = await client.PostAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new CustomizeException(Failure.EmailSendFailing);
        }
    }
    #endregion
}
