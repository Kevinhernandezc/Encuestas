using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Encuestas.credenciales
{
    public class autorizacionToken : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var acceso = (context.UserName == "Admin" && context.Password == "123");
            if (!acceso)
            {
                context.SetError("invalid_grant", "The username or password is incorrect.");
                return;
            }

            ClaimsIdentity identidad = new ClaimsIdentity(context.Options.AuthenticationType);
            identidad.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

            if (context.UserName == "Admin" && context.Password == "123")
            {
                identidad.AddClaim(new Claim(ClaimTypes.Role, "ADMINISTRADOR"));
            }

            context.Validated(identidad);
        }
    }
}
