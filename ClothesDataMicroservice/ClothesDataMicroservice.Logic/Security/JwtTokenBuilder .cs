using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClothesDataMicroservice.Logic.Security
{
    public class JwtTokenBuilder : ITokenBuilder
    {
        public string BuildToken(string email)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("something-blablabla-i-will-do-it-later-or-never"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
            };
            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
