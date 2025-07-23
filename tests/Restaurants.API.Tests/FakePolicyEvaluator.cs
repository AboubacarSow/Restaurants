using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.API.Tests;

internal class FakePolicyEvaluator : IPolicyEvaluator
{
    public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
        List<Claim> claims = [
            new (ClaimTypes.NameIdentifier, "1"),
            new (ClaimTypes.Role, "Admin")
            ];
        var claimsPrincipal = new ClaimsPrincipal();
        claimsPrincipal.AddIdentity(new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier,"1"),
                new Claim(ClaimTypes.Role,"Admin")
            ]
            ));
        
        var ticket = new AuthenticationTicket(claimsPrincipal, "Test");
        var result = AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }

    public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
    {
        var result = PolicyAuthorizationResult.Success();
        return Task.FromResult(result);
    }
}
