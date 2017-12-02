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
        IMeetingsManipulation _meetingsManipulation { get; set; }

        public MeetingsController(IMeetingsManipulation meetingsManipulation)
        {
            _meetingsManipulation = meetingsManipulation;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_meetingsManipulation.GetMeetings());
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
                _meetingsManipulation.Create(model);
                return Ok("New meeting created");
            }
            catch(Exception ex)
            {
                // log exception
            }
            return BadRequest();

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MeetingDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _meetingsManipulation.Update(id, model);
                return Ok("Meeting updated");
            }
            catch (Exception ex)
            {
                // log exception
            }

            return BadRequest();
        }
    }
}
