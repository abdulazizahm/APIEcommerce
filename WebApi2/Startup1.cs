using DAL.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(WebApi2.Startup1))]

namespace WebApi2
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            //1)
            app.UseCors(CorsOptions.AllowAll);
            //2)
            //authnaticated "Faculty"
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                AllowInsecureHttp = true,
                Provider = new OAuthProvider()
            });///login =>create token =>expirect


            //security
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            //3)last Middle 
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();//[Route()]
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}"
                , new { id = RouteParameter.Optional });
            app.UseWebApi(config);
        }
    }

    internal class OAuthProvider : OAuthAuthorizationServerProvider
    {
        //valildate Client_id
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //
            context.Validated();//validate all client_id
        }
        //login (username,password,grant_type :"password")
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //check user / passwor
            context.OwinContext.Response.Headers.Add(" Access - Control - Allow - Origin",new[] { "*"});
            ApplicationDbContext db = new ApplicationDbContext();
            UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(db);
            UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);
            try
            {
                ApplicationUser identityUser = await manager.FindAsync(context.UserName, context.Password);
                if (identityUser == null)
                {
                    context.SetError("grant_type", "Username & password incorrect");
                }
                else
                {
                    //card
                    ClaimsIdentity claims = new ClaimsIdentity(context.Options.AuthenticationType);
                    //set field
                    claims.AddClaim(new Claim(ClaimTypes.Name, identityUser.UserName));
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, identityUser.Id));
                    if (manager.IsInRole(identityUser.Id, "Admin"))
                    {
                        claims.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                    }
                    if (manager.IsInRole(identityUser.Id, "Vendor"))
                    {
                        claims.AddClaim(new Claim(ClaimTypes.Role, "Vendor"));
                    }
                    //token
                    context.Validated(claims);//token 
                }
            }
            catch (Exception ex)
            {
                context.SetError("grant_type", ex.Message);
            }
          
         
        }
    }
}
