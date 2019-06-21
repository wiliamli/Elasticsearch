using Jwell.Application.Services;
using Jwell.Application.Services.Params;
using Jwell.Framework.Mvc;
using Jwell.FullTextSearch.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Jwell.Sample.Controllers
{
    public class RegisterController : BaseApiController
    {
        private ITeamService TeamService { get; set; }

        public RegisterController(ITeamService teamService)
        {
            this.TeamService = teamService;
        }

        [AllowAnonymous]
        [HttpPost]
        public StandardJsonResult<string> Login([FromBody]LoginParam param)
        {
            bool IsSuccess = false;
            string errorMsg = string.Empty;
            var result =  StandardAction(() => {
                var team = this.TeamService.GetTeam(param.TeamCode, param.Password);
                if (team != null) {
                    HttpContext.Current.Session["loginUser"] = team.TeamNumber;
                    IsSuccess = true;
                    return HttpContext.Current.Session.SessionID;
                }
                errorMsg = "登陆失败，团队标识或者密码输入错误";
                return string.Empty;
            });

            result.Success = IsSuccess;
            result.Message = errorMsg;
            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        public void Add()
        {
            this.TeamService.add();
        }

        [HttpGet]
        public StandardJsonResult LogOut()
        {
            bool IsSuccess = false;
            string errorMsg = string.Empty;
            var result = StandardAction(() => {
                if (HttpContext.Current.Session["loginUser"] == null)
                {
                    errorMsg = "注销失败";
                    return;
                }
                HttpContext.Current.Session.Abandon();
                IsSuccess = true;
            });

            result.Success = IsSuccess;
            result.Message = errorMsg;
            return result;
        }
    }
}
