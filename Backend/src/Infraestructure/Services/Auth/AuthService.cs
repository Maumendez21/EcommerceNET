using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Models.Token;
using Ecommerce.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infraestructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        public JwtSettings _jwtSettings { get; }
        //recibe el request del cliente
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(IHttpContextAccessor httpContextAccessor, IOptions<JwtSettings> jwtSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtSettings = jwtSettings.Value;
        }
        public string CreateToken(User user, IList<string>? roles)
        {
            List<Claim> claims  = new List<Claim>(){
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName!),
                new Claim("userId", user.Id),
                new Claim("email", user.Email)
            };

            foreach (var role in roles!)
            {
                var claim = new Claim(ClaimTypes.Role, role);
                claims.Add(claim);
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.key!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.ExpireTime),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);

        }

        public string GetSessionUser()
        {
            return _httpContextAccessor.HttpContext!.User?.Claims?
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value!;
        }
    }
}