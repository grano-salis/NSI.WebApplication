using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using NSI.DC.HearingsRepository;
using NSI.DC.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/hearings")]
    public class HearingsController : Controller
    {
        IHearingsManipulation _hearingsManipulation { get; set; }

        public HearingsController(IHearingsManipulation hearingsManipulation)
        {
            _hearingsManipulation = hearingsManipulation;
        }

        [HttpPost]
        public IActionResult Post([FromBody] HearingDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(new NSIResponse<HearingDto>()
                {
                    Data = _hearingsManipulation.CreateHearing(model),
                    Message = "New hearing created"
                });
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                return BadRequest(new NSIResponse<object> { Data = null, Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] HearingDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(new NSIResponse<HearingDto>()
                {
                    Data = _hearingsManipulation.UpdateHearing(id, model),
                    Message = "Hearing updated"
                });
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                return BadRequest(new NSIResponse<object> { Data = null, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("case/{caseId}")]
        public IActionResult GetHearingsByCase(int caseId)
        {
            try
            {
                return Ok(new NSIResponse<ICollection<HearingDto>>()
                {
                    Data = _hearingsManipulation.GetHearingsByCase(caseId),
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
        public IActionResult GetAll()
        {
            try
            {
                return Ok(new NSIResponse<ICollection<HearingDto>>()
                {
                    Data = _hearingsManipulation.GetHearings(),
                    Message = "Success"
                });
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                return BadRequest(new NSIResponse<object> { Data = null, Message = ex.Message });
            }

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(new NSIResponse<HearingDto>()
                {
                    Data = _hearingsManipulation.GetHearingById(id),
                    Message = "Success"
                });
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
                return BadRequest(new NSIResponse<object> { Data = null, Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHearing(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _hearingsManipulation.Delete(id);
                return Ok(new NSIResponse<object>()
                {
                    Data = null,
                    Message = "Hearing deleted"
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
