using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using WannaTravel.BusinessLogic.Interfaces;
using WannaTravel.BusinessLogic.Services;
using WannaTravel.Repositories.EF.Repos;
using WannaTravel.Repositories.Interfaces;

namespace WannaTravel.Api.App_Start
{

    public class AutofacConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<CommentCollection>().As<ICommentCollection>().SingleInstance();

            builder.RegisterGeneric(typeof(GenericRepository<>))
                    .As(typeof(IGenericRepository<>))
                    .InstancePerDependency();
            builder.RegisterType<CommentRepository>().As<ICommentRepository>();

            builder.RegisterType<QueueWriter>().SingleInstance();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}