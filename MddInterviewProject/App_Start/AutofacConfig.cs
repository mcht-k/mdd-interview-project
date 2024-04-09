using Autofac;
using Autofac.Integration.Mvc;
using MddInterviewProject.Services;
using System.Net.Http;
using System.Web.Mvc;
using MddInterviewProject;

namespace MddInterviewProject
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<HttpClient>().SingleInstance();
            builder.RegisterType<BlogService>().As<IBlogService>();
            builder.Register(c => new Logger(typeof(MvcApplication))).As<ILogger>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}