using MicrosServices.APIGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MicrosServices.APIGateway.Services
{
    public interface ITokenHelper
    {
        ComplexToken CreateToken(TokenApp user);
        ComplexToken CreateToken(Claim[] claims);
        Token RefreshToken(ClaimsPrincipal claimsPrincipal);

    }
}
