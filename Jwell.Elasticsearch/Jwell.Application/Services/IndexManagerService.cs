using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Params;
using Jwell.Domain.Entities;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Domain.Uow;
using Jwell.Framework.Paging;
using Jwell.Modules.Cache;
using Jwell.Modules.Elasticsearch.Entity;
using Jwell.Modules.Elasticsearch.Helper;
using Jwell.Modules.Elasticsearch.SearchProvider;
using Jwell.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    /// <summary>
    /// 索引管理-服务
    /// </summary>
    public class IndexManagerService : ApplicationService, IIndexManagerService
    {
        private IIndexManagerRepository IndexManagerRepository { get; set; }
        private ICacheClient CacheClient { get; set; }

        public IndexManagerService(IIndexManagerRepository indexManagerRepository,
            ICacheClient cacheClient)
        {
            this.IndexManagerRepository = indexManagerRepository;
            this.CacheClient = cacheClient;
        }

        [UnitOfWork(IsDisabled =false)]
        public bool AddIndex(IndexAddParam param, ref string errorMsg)
        {
            if (this.IsIndexExist(param.ServiceNumber, param.IndexName))
            {
                errorMsg = $"该服务已存在名称为{param.IndexName}的索引";
                return false;
            }

            var entity = new IndexManager
            {
                ServiceNumber = param.ServiceNumber,
                ServiceSign = param.ServiceSign,
                ShardNumber = param.ShardNum,
                Size = param.Size,
                Status = IndexStatus.NotStarted,
                ExpensionNum = 0,
                MaxExpensionNum = param.MaxExpensionNum,
                ReplicasNumber = param.ReplicasNum,
                IndexName = param.IndexName,
                IndexNumber = Guid.NewGuid().ToString("N"),
                IsDeleted = false,
                CreatedTime = DateTime.Now,
                CreatedBy = "admin",
                ModifiedTime = DateTime.Now,
                ModifiedBy = "admin"
            };

            return this.IndexManagerRepository.AddWithTransection(entity, () =>
            {
                var key = IndexNameHelper.GetIndexMappingName(entity.ServiceSign, entity.IndexName);
                var isSucess = this.CacheClient.SetCache(key, entity.Status, 173000);
                if (!isSucess)
                    throw new Exception("缓存更新失败");
            });
        }

        [UnitOfWork(IsDisabled = false)]
        public bool Delete(string indexNumber)
        {
            var entity = this.IndexManagerRepository.Queryable()
               .Where(x => x.IsDeleted == false && x.IndexNumber == indexNumber.Trim())
               .FirstOrDefault();

            if (entity == null)
                return false;

            entity.Status = IndexStatus.Deleted;
            entity.IsDeleted = true;
            entity.ModifiedBy = "admin";
            entity.ModifiedTime = DateTime.Now;

            var indexMappingName = IndexNameHelper.GetIndexMappingName(entity.ServiceSign, entity.IndexName);
            var isSuccess = this.IndexManagerRepository.ModifyWithTransection(entity, () =>
            {
                if (!this.CacheClient.SetCache(indexMappingName, entity.Status, 173000))
                {
                    throw new Exception("缓存更新失败");
                }
            });

            if (isSuccess)
                new Task(() =>
                {
                    try
                    {
                        IndexOperateHelper.RemoveIndex(indexMappingName);
                    }
                    catch (Exception)
                    {
                        //TODO:
                    }

                }).Start();

            return isSuccess;
        }

        [UnitOfWork(IsDisabled = false)]
        public bool Expension(string indexNumber,ref string errorMsg)
        {
            var entity = this.IndexManagerRepository.Queryable()
              .Where(x => x.IsDeleted == false && x.IndexNumber == indexNumber.Trim())
              .FirstOrDefault();

            if (entity == null)
                return false;

            if (entity.MaxExpensionNum == entity.ExpensionNum)
            {
                errorMsg = $"{entity.IndexName}已进行{entity.ExpensionNum}次扩容，已达到该索引的最大扩容次数";
                return false;
            }

            entity.ExpensionNum += 1;
            entity.ModifiedBy = "admin";
            entity.ModifiedTime = DateTime.Now;

            return this.IndexManagerRepository.ModifyWithTransection(entity, () =>
            {
                var key = IndexNameHelper.GetIndexSizeName(entity.ServiceSign, entity.IndexName);
                if (!this.CacheClient.SetCache<int>(key, entity.Size * (entity.ExpensionNum + 1), 173000))
                    throw new Exception("缓存设置失败");
            });
        }

        public PageResult<IndexListDto> GetIndexByPage(SearchIndexParams param)
        {
            var query = this.IndexManagerRepository.Queryable().Where(x => x.IsDeleted == false && x.ServiceNumber == param.ServiceNumber)
                .Select(x => new IndexListDto
                {
                    IndexName = x.IndexName,
                    IndexNumber = x.IndexNumber,
                    LastModifiedTime = x.ModifiedTime,
                    EnumStatus = x.Status,
                    ServiceSign = x.ServiceSign
                });
            if (!string.IsNullOrEmpty(param.IndexName))
                query = query.Where(x => x.IndexName.Contains(param.IndexName));

            return query.ToPageDtos(param);
        }

        public IndexDetailDto GetIndexDetail(string indexNumber)
        {
            var entity = this.IndexManagerRepository.Queryable().Where(x => x.IsDeleted == false && x.IndexNumber == indexNumber)
                .Select(x => new IndexDetailDto
                {
                    IndexName = x.IndexName,
                    ShardNum = x.ShardNumber,
                    Size = x.Size,
                    ExpentionNum = x.ExpensionNum,
                    MaxExpensionNum = x.MaxExpensionNum,
                    ReplicasNum = x.ReplicasNumber,
                    EnumStatus = x.Status
                }).FirstOrDefault();
            if (entity != null)
            {
                entity.Status = entity.EnumStatus.GetDescription();
            }
            return entity;
        }

        public IndexDto GetOne(string indexNumber)
        {
            var entity = this.IndexManagerRepository.Queryable().Where(x => x.IsDeleted == false && x.IndexNumber == indexNumber)
                .Select(x => new IndexDto
                {
                    IndexName = x.IndexName,
                    ShardNum = x.ShardNumber,
                    Size = x.Size,
                    MaxExpensionNum = x.MaxExpensionNum,
                    ReplicasNum = x.ReplicasNumber,
                    IndexNumber = x.IndexNumber
                }).FirstOrDefault();
            return entity;
        }
        
        public bool Modify(string indexNumber, IndexModifyParam param, ref string errorMsg)
        {
            var entity = this.IndexManagerRepository.Queryable()
                .Where(x => x.IsDeleted == false && x.IndexNumber == indexNumber.Trim())
                .FirstOrDefault();
            if (entity == null)
            {
                errorMsg = $"该索引不存在或者已被删除";
                return false;
            }

            entity.IndexName = param.IndexName;
            entity.Size = param.Size;
            entity.ShardNumber = param.ShardNum;
            entity.ReplicasNumber = param.ReplicasNum;
            entity.MaxExpensionNum = param.MaxExpensionNum;

            return this.IndexManagerRepository.Update(entity) > 0;
        }

        [UnitOfWork(IsDisabled = false)]
        public bool StartIndex(string indexNumber)
        {
            var entity = this.IndexManagerRepository.Queryable()
               .Where(x => x.IsDeleted == false && x.IndexNumber == indexNumber.Trim())
               .FirstOrDefault();

            if (entity == null)
                return false;

            entity.Status = IndexStatus.Started;
            entity.ModifiedBy = "admin";
            entity.ModifiedTime = DateTime.Now;

            return this.IndexManagerRepository.ModifyWithTransection(entity, () =>
            {
                var key = IndexNameHelper.GetIndexMappingName(entity.ServiceSign, entity.IndexName);
                var sizeKey = IndexNameHelper.GetIndexSizeName(entity.ServiceSign, entity.IndexName);

                if (!this.CacheClient.SetCache<int>(sizeKey, entity.Size * (entity.ExpensionNum + 1), 173000))
                    throw new Exception("缓存设置失败");

                if (!this.CacheClient.SetCache(key, entity.Status, 173000))
                    throw new Exception("缓存设置失败");
            });
        }

        [UnitOfWork(IsDisabled = false)]
        public bool StopIndex(string indexNumber)
        {
            var entity = this.IndexManagerRepository.Queryable()
               .Where(x => x.IsDeleted == false && x.IndexNumber == indexNumber.Trim())
               .FirstOrDefault();

            if (entity == null)
                return false;

            entity.Status = IndexStatus.Stoped;
            entity.ModifiedBy = "admin";
            entity.ModifiedTime = DateTime.Now;

            return this.IndexManagerRepository.ModifyWithTransection(entity, () =>
            {
                var key = IndexNameHelper.GetIndexMappingName(entity.ServiceSign, entity.IndexName);
                var isSuccess = this.CacheClient.SetCache(key, entity.Status, 173000);
                if (!isSuccess)
                    throw new Exception("缓存更新失败");
            });
        }

        private bool IsIndexExist(string serviceNumber, string indexName)
        {
            return this.IndexManagerRepository.Queryable()
                .Any(x => x.ServiceNumber == serviceNumber && x.IndexName == indexName && x.IsDeleted == false);
        }
    }
}
