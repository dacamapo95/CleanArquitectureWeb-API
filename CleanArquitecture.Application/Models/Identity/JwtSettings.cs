namespace CleanArquitecture.Application.Models.Identity;

public class JwtSettings
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Duración en minutos
    /// </summary>
    public double Duration { get; set; }
}
