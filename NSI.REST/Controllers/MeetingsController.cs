using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using NSI.DC.Exceptions;
using NSI.DC.MeetingsRepository;
using NSI.DC.Response;
using NSI.REST.Models;
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
                return Ok(new NSIResponse<MeetingDto>()
                {
                    Data = _meetingsManipulation.GetMeetingById(id),
                    Message = "Success"
                });
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

        [HttpGet("getMeetingWithPaging")]
        [ProducesResponseType(typeof(NSIResponse<ICollection<MeetingDto>>), 200)]
        public IActionResult GetMeetings(
         [FromQuery] int? page,
         [FromQuery] int? pageSize)
        {
            try
            {
                return Ok(new NSIResponse<ICollection<MeetingDto>> { Data = _meetingsManipulation.GetMeetings(page, pageSize), Message = "Success" });
            }
            catch (NSIException ex)
            {
                Logger.Logger.LogError(ex);
                if (ex.ErrorType == DC.Exceptions.Enums.ErrorType.MissingData)
                    return NoContent();
                return BadRequest(new NSIResponse<object> { Data = null, Message = "Parameter error!" });
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex);
                return StatusCode(500, new NSIResponse<object> { Data = null, Message = ex.Message });
            }
        }


        [HttpPost]
        [Route("searchMeeting")]
        public IActionResult Search([FromBody]MeetingsSearchModel model, int pageNumber, int pageSize)
        {
            try
            {
                if (model == null)
                    throw new NSIException("MeetingSearchModel is null", DC.Exceptions.Enums.Level.Error, DC.Exceptions.Enums.ErrorType.InvalidParameter);

                MeetingDto meetingDto = new MeetingDto()
                {
                    MeetingId = model.MeetingId,
                    To = model.To,
                    Title = model.Title,
                    From = model.From,
                    UserMeeting = model.UserMeeting.Select(x => new UserMeetingDto()
                    {
                        UserId = x.UserId,
                        UserName = x.UserName
                    })
                };

                return Ok(new NSIResponse<ICollection<MeetingDto>> { Data = _meetingsManipulation.SearchMeetings(meetingDto, pageNumber, pageSize), Message = "Success" });
            }
            catch (NSIException ex)
            {
                Logger.Logger.LogError(ex);
                if (ex.ErrorType == DC.Exceptions.Enums.ErrorType.MissingData)
                    return NoContent();
                return BadRequest(new NSIResponse<object> { Data = null, Message = "Parameter error!" });
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex);
                return StatusCode(500, new NSIResponse<object> { Data = null, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("user/{userId}")]
        public IActionResult GetMeetingsByUser(int userId)
        {
            try
            {
                return Ok(new NSIResponse<ICollection<MeetingDto>>()
                {
                    Data = _meetingsManipulation.GetMeetingsByUser(userId),
                    Message = "Success"
                });
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                return BadRequest(new NSIResponse<object> { Data = null, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("getMeetingTimes")]
        public IActionResult GetMeetingsTimes(ICollection<int> userIds, DateTime from, DateTime to, int meeetingDuration)
        {
            try
            {
                return Ok(new NSIResponse<ICollection<MeetingTimeDto>>()
                {
                    Data = _meetingsManipulation.GetMeetingTimes(userIds, from, to, meeetingDuration),
                    Message = "Success"
                });
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                return BadRequest(new NSIResponse<object> { Data = null, Message = ex.Message });
            }
        }


    }
}
