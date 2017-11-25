using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using NSI.DC.MeetingsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/meetings")]
    public class MeetingsController : Controller
    {
        private readonly IMeetingsManipulation _meetingsManipulation;

        public MeetingsController(IMeetingsManipulation meetingsManipulation)
        {
            _meetingsManipulation = meetingsManipulation;
        }

        [HttpPost]
        public IActionResult Post([FromBody] MeetingDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = _meetingsManipulation.Create(model);
                if (result != null)
                    return Ok(result);
            }
            catch(Exception ex)
            {
                // log exception
            }
            return BadRequest();

        }
    }
}
