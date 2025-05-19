namespace Core;

public class JwtSetting
{
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public double ExpireMinutes { get; set; }
    public int RefreshTokenExpireDays { get; set; }
    public string Secret { get; set; } = null!;
}
