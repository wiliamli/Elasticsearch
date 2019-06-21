using Autofac;
using Jwell.Framework.Modules;
using Jwell.Modules.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Elasticsearch
{
    [DependOn(typeof(JwellCacheModule))]
    public class JwellElasticsearchModule: JwellModule
    {
        public override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
