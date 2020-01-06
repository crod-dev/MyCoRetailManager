using Microsoft.AspNet.Identity;
using MRMDataManager.Library.DataAccess;
using MRMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

// 13 Creation of Controller to handle User requests
namespace TRMDataManager.Controllers
{
    [Authorize]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    { 
        // 13 Use model in library instead a UI model, UI (display) models are modified to make the properties more reader friendly
        // 13 Library model 
        [HttpGet]
        public UserModel GetById()
        {
            // 13 Gets the id of the current user logged in
            string id = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();

            return data.GetUserById(id).First();
        }
    }
}
