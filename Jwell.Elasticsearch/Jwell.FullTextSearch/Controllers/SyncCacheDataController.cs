using Jwell.Application.Services;
using Jwell.Framework.Mvc;
using Jwell.FullTextSearch.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jwell.Sample.Controllers
{
    public class SyncCacheDataController : BaseApiController
    {

        private ISyncCacheDataService SyncCacheDataService { get; set; }

        public SyncCacheDataController(ISyncCacheDataService syncCacheDataService)
        {
            this.SyncCacheDataService = syncCacheDataService;
        }
        // GET: api/SyncData
        public StandardJsonResult Get()
        {
            return StandardAction(() => {
                this.SyncCacheDataService.SyncCacheData();
            });
        }
    }
}
