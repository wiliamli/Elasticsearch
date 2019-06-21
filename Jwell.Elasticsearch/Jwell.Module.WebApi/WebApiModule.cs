using Autofac;
using Autofac.Integration.WebApi;
using System.Web.Http;

namespace Jwell.Modules.WebApi
{
    public class WebApiModule : Framework.Modules.JwellModule
    {

        public override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            INI.IniConfig.ReadValue("ProjectSign");
        }

        public override void Loaded(IContainer container)
        {
            base.Loaded(container);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            INI.IniConfig.ReadValue("ProjectSign");
        }
    }
}
