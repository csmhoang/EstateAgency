namespace Core;

public interface IAppSettingServices
{
    JwtSetting JwtSetting { get; }
    CloudinarySetting CloudinarySetting { get; }
    MailJetOption MailJetOption { get; }
}
