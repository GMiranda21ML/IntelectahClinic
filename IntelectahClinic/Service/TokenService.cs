
using IntelectahClinic.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IntelectahClinic.Service
{
    public class TokenService
    {
        public string GenerarToken(Paciente usuario)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("id", usuario.Id),
                new Claim("nomeCompleto", usuario.NomeCompleto)
            };

            SymmetricSecurityKey chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sae4m5(m^wbqo^p(t&as+cwhay!t$z^!w4@@$6bvcn%w$y3j+-"));

            SigningCredentials signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken
                (
                    expires: DateTime.Now.AddHours(1),
                    claims: claims,
                    signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
