using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using NSI.DC.HearingsRepository;
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
                _hearingsManipulation.Create(model);
                return Ok("New hearing created");
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
            }
            return BadRequest();

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
                _hearingsManipulation.Update(id, model);
                return Ok("Hearing updated");
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
            }

            return BadRequest();
        }

        [HttpGet("/case/{caseId}", Name = "GetHearingsByCase")]
        public IActionResult GetHearingsByCase(int caseId)
        {
            try
            {
                var hearings = _hearingsManipulation.GetHearingsByCase(caseId);
                if (hearings != null)
                    return Ok(hearings);
            }
            catch(Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
            }
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var hearings = _hearingsManipulation.GetHearings();
            if (hearings != null)
                return Ok(hearings);

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var hearing = _hearingsManipulation.GetHearingById(id);
            if (hearing != null)
                return Ok(hearing);

            return NoContent();
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
                return Ok("Meeting deleted");
            }
            catch (Exception ex)
            {
                Logger.Logger.LogError(ex.Message);
            }

            return BadRequest();
        }
    }
}
