using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockBrokarageChallenge.Application.UseCases.LoginContext.Outputs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockBrokarageChallenge.Application.Shared.Security
{
    public static class GenerateBearerToken
    {
        public static LoginOutput GenerateJwt(IConfiguration configuration, int customerId, int accountId, int accountNumber) 
        {
            //declarações do usuário
            var claims = new[]
            {
            new Claim("customerId", $"{customerId}"),
            new Claim("accountId", $"{accountId}"),
            new Claim("accountNumber", $"{accountNumber}"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) //Id do token
        };

            //gerar chave privada para assinar o token
            var privateKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])); //Formato em bytes, cryptografado

            //gerar a assinatura digital
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //definir o tempo de expiração
            var expiration = DateTime.UtcNow.AddMinutes(60);

            //gerar o token
            JwtSecurityToken token = new JwtSecurityToken(
                //emissor
                issuer: configuration["Jwt:Issuer"],
                //audiencia
                audience: configuration["Jwt:Audience"],
                //claims
                claims: claims,
                //data de expiracao
                expires: expiration,
                //assinatura digital
                signingCredentials: credentials
                );
            var tokenFinal = new JwtSecurityTokenHandler().WriteToken(token);
            return new LoginOutput(tokenFinal, expiration);
        }
    }
}
