using Jwell.Application.Services.Dtos;
using Jwell.Application.Services.Params;
using Jwell.Domain.Entities;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Paging;
using Jwell.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    public class ServiceInfoService : ApplicationService, IServiceInfoService
    {
        private IServiceInfoRepository ServiceInfoRepository { get; set; }
        public ServiceInfoService(IServiceInfoRepository serviceInfoRepository)
        {
            this.ServiceInfoRepository = serviceInfoRepository;
        }

        public bool Add(ServiceInfoAddParams dto, string teamNumber, ref string errorMsg)
        {
            var isExist = this.ServiceInfoRepository.Queryable()
                .Any(x => x.IsDeleted == false && x.ServiceNumber == dto.ServiceNumber);

            if (isExist)
            {
                errorMsg = $"该服务编号为:{dto.ServiceNumber}的服务已存在";
                return false;
            }
            var entity = new ServiceInfo
            {
                ServiceCode = dto.ServiceCode,
                ServiceNumber = dto.ServiceNumber,
                IsDeleted = false,
                Remark = dto.Remark,
                TeamNumber = teamNumber.Trim(),
                CreatedBy = "admin",
                CreatedTime = DateTime.Now,
                ModifiedBy = "admin",
                ModifiedTime = DateTime.Now
            };
            return ServiceInfoRepository.Add(entity) > 0;
        }

        public PageResult<ServiceInfoDto> GetListByPage(SearchServiceInfoParams param, string teamNumber)
        {
            var query = this.ServiceInfoRepository.Queryable().Where(x => x.IsDeleted == false && x.TeamNumber == teamNumber.Trim())
                .Select(x => new ServiceInfoDto
                {
                    ServiceCode = x.ServiceCode,
                    ServiceNumber = x.ServiceNumber,
                    Remark = x.Remark,
                    LastModifiedTime = x.ModifiedTime
                });
            if (!string.IsNullOrEmpty(param.ServiceCode))
                query = query.Where(x => x.ServiceCode == param.ServiceCode.Trim());
            return query.ToPageDtos(param);
        }
    }
}
