using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using EmailLog.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EmailLog.Startup))]

namespace EmailLog
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                ReturnUrlParameter = "returnUrl",
                AuthenticationType = "ApplicationCookie",
                CookieName = "EmailLog",
                LoginPath = new PathString("/Auth/Login")
            });

            FilterConfig.Configure(GlobalFilters.Filters);

        }
    }
}
