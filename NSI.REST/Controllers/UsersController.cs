using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUsersManipulation _usersManipulation;

        public UsersController(IUsersManipulation usersManipulation)
        {
            _usersManipulation = usersManipulation;
        }

        [HttpGet]
        [Route("meetings")]
        public IActionResult GetForMeetings(string username)
        {
            return Ok(_usersManipulation.GetForMeetings(username));
        }
    }
}
