using System;
using System.Security.Claims;
using System.Web.Helpers;
using MDoc.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace MDoc.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            var cookieOptions = new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/Account/LogOn"),
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                SlidingExpiration = false,
                ExpireTimeSpan = TimeSpan.FromHours(24),
                CookieName = "MDoc"
            };
            app.SetDefaultSignInAsAuthenticationType(cookieOptions.AuthenticationType);
            app.UseCookieAuthentication(cookieOptions);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}