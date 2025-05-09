using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace OutsideInTestingDemo.Tests.Bootstrapping;

public static class FakeJwtToken
{
    public static string Issuer { get; } = Guid.NewGuid().ToString();
    public static string Audience { get; } = Guid.NewGuid().ToString();
    public static SecurityKey SecurityKey { get; }
    public static SigningCredentials SigningCredentials { get; }

    private static readonly JwtSecurityTokenHandler _tokenHandler = new();

    static FakeJwtToken()
    {
        var rsa = RSA.Create();
        SecurityKey = new RsaSecurityKey(rsa) { KeyId = Guid.NewGuid().ToString() };
        SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.RsaSha256);
    }

    public static string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        return _tokenHandler.WriteToken(new JwtSecurityToken(Issuer, Audience, claims, null, DateTime.UtcNow.AddMinutes(20), SigningCredentials));
    }

    public static JwtSecurityToken ReadJwtToken(string token)
    {
        return _tokenHandler.ReadJwtToken(token);
    }
}
