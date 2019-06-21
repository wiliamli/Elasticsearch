using Jwell.Domain.Entities;
using Jwell.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    public class TeamService : ITeamService
    {
        private ITeamRepository TeamRepository { get; set; }

        public TeamService(ITeamRepository teamRepository)
        {
            this.TeamRepository = teamRepository;
        }

        public Team GetTeam(string teamCode, string password)
        {
            var query = this.TeamRepository.Queryable().Where(x => x.IsDeleted == false
                && x.TeamCode == teamCode.Trim()
                && x.Password == password.Trim());

            return query.FirstOrDefault();
        }

        public void add()
        {
            this.TeamRepository.Add(new Team
            {
                CreatedBy = "admin",
                CreatedTime = DateTime.Now,
                IsDeleted = false,
                LeaderEmployeeId = "123456",
                LeaderName = "test",
                ModifiedBy = "admin",
                ModifiedTime = DateTime.Now,
                Password = "123",
                TeamCode = "testTeam",
                TeamNumber = "8adec707e292458cb12f36af09f6cd86"
            });
        }
    }
}
