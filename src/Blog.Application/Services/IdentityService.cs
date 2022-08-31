using Blog.Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Application.Services
{
    public class IdentityService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly byte[] _key;
        public IdentityService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);
        }

        public JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();

        public string WritenToken(SecurityToken securityToken)
        {
            return TokenHandler.WriteToken(securityToken);
        }

        public SecurityToken CreateSecurityToken(ClaimsIdentity claimsIdentity)
        {
            var tokenDescriptor = GetSecurityTokenDescriptor(claimsIdentity);
            return TokenHandler.CreateToken(tokenDescriptor);
        }

        private SecurityTokenDescriptor GetSecurityTokenDescriptor(ClaimsIdentity claimsIdentity)
        {
            return new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.Now.AddHours(2),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audiences[0],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key),
                                             SecurityAlgorithms.HmacSha256Signature)
            };
        }


    }
}
