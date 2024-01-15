namespace CleanArquitecture.Application.Models.Identity;

public record JwtSettings
{
    public string Key { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public int Duration { get; init; }
}
