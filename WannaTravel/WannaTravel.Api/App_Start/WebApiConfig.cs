using System.Linq;
using System.Net.Http.Formatting;
using System.Threading;
using System.Web.Http;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using WannaTravel.Api.App_Start;
using WannaTravel.BusinessLogic.Services;

namespace WannaTravel.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, IAppBuilder app)
        {
            // Web API configuration and services
            AutofacConfig.Configure(config);

            var qw = (QueueWriter)config.DependencyResolver.GetService(typeof(QueueWriter));

            var context = new OwinContext(app.Properties);
            var token = context.Get<CancellationToken>("host.OnAppDisposing");
            if (token != CancellationToken.None)
            {
                LastDataInsert(token, qw);
            }

            qw.RunTask(token);

            // Web API routes
            config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private static void LastDataInsert(CancellationToken token, QueueWriter writer)
        {
            token.Register(() =>
            {
                writer.WriteToDbAsync(true).GetAwaiter().GetResult();
            });
        }
    }
}
