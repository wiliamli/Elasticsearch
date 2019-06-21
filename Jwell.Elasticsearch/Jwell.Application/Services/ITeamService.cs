using Jwell.Domain.Entities;
using Jwell.Framework.Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Application.Services
{
    public interface ITeamService: IApplicationService
    {
        Team GetTeam(string teamCode, string password);

        void add();
    }
}
