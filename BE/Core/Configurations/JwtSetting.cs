namespace Core;

public class JwtSetting
{
    public string ValidIssuer { get; set; } = null!;
    public string ValidAudience { get; set; } = null!;
    public double Expires { get; set; }
    public string Secret { get; set; } = null!;
}
