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
            var meetings = _meetingsManipulation.GetMeetings();
            if (meetings != null)
                return Ok(meetings);

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var meeting = _meetingsManipulation.GetMeetingById(id);
            if (meeting != null)
                return Ok(meeting);

            return NoContent();
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
                Logger.Logger.LogError(ex.Message);
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
                Logger.Logger.LogError(ex.Message);
            }

            return BadRequest();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteMeeting(int id)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _meetingsManipulation.Delete(id);
                return Ok("Meeting deleted");
            }
            catch(Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
            }

            return BadRequest();
        }
    }
}
