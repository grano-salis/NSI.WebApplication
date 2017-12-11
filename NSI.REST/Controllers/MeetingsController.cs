using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using NSI.DC.Exceptions;
using NSI.DC.MeetingsRepository;
using NSI.DC.Response;
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
            try {
                return Ok(new NSIResponse<ICollection<MeetingDto>>() {
                    Data = _meetingsManipulation.GetMeetings(),
                    Message = "Success"
                });
            }
            catch(Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                return StatusCode(500, new NSIResponse<object> { Data = null, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try { 
                var meeting = _meetingsManipulation.GetMeetingById(id);
                if (meeting != null)
                    return Ok(meeting);

                return NoContent();
            }
            catch(NSIException ex)
            {
                Logger.Logger.LogError(ex.Message);
                return BadRequest(new NSIResponse<object> { Data = null, Message = ex.Message });
            }
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
                return Ok(new NSIResponse<MeetingDto>()
                {
                    Data = _meetingsManipulation.CreateMeeting(model),
                    Message = "New meeting created"
                });
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                return BadRequest(new NSIResponse<object> { Data = null, Message = ex.Message });
            }

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
                return Ok(new NSIResponse<MeetingDto>()
                {
                    Data = _meetingsManipulation.EditMeeting(id, model),
                    Message = "Meeting updated"
                });
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                return BadRequest(new NSIResponse<object> { Data = null, Message = ex.Message });
            }
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
                _meetingsManipulation.RemoveMeeting(id);
                return Ok(new NSIResponse<object>()
                {
                    Data = null,
                    Message = "Meeting deleted"
                });
            }
            catch(Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                return BadRequest(new NSIResponse<object> { Data = null, Message = ex.Message });
            }
        }
    }
}
