using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieRentalAppUI.Services
{
    public class TokenService : ITokenService
    {
        public IEnumerable<Claim> GetTokenClaims(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(jwtToken);

            return jwtSecurityToken.Claims;
        }
    }
}
