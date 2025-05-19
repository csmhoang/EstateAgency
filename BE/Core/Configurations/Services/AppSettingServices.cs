using Microsoft.Extensions.Configuration;

namespace Core;

public class AppSettingServices(IConfiguration configuration) : IAppSettingServices
{
    public JwtSetting JwtSetting => configuration.GetSection("JwtSettings").Get<JwtSetting>() ?? new JwtSetting();
    public CloudinarySetting CloudinarySetting => configuration.GetSection("CloudinarySettings").Get<CloudinarySetting>() ?? new CloudinarySetting();
    public MailJetOption MailJetOption => configuration.GetSection("MailJetSettings").Get<MailJetOption>() ?? new MailJetOption();
}
