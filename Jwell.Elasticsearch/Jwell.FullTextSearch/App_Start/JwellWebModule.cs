using Jwell.Application;
using Jwell.Framework.Modules;
using Jwell.Modules.WebApi;
using Jwell.Modules.MVC;
using Jwell.Repository;
using Jwell.Modules.MessageQueue;

namespace Jwell.FullTextSearch
{
    /// <summary>
    /// 模块加载
    /// </summary>
    [DependOn(typeof(MvcModule),typeof(WebApiModule),
        typeof(JwellApplicationModule),typeof(JwellRepositoryModule),
        typeof(JwellMessageQueueModule))]
    public class JwellWebModule : JwellModule
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }
    }
}