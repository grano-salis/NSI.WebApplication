using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return DateTime.Now.ToString();
        }

        [HttpPost("postURL")]
        public string PostURL([FromBody]PostModel model)
        {
            return model.b;
        }
    }    

    public class PostModel
    {
        public int a { get; set; }
        public string b { get; set; }
    }
}