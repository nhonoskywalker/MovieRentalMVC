using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieRentalAppUI.Services
{
    public interface ITokenService
    {
        public IEnumerable<Claim> GetTokenClaims(string token);
    }
}
