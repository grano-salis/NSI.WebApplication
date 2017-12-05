using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using NSI.REST.Models;
using NSI.Repository;
using NSI.DC.CaseRepository;
using NSI.BLL.Interfaces;
using NSI.Repository;
using System.Net.Http;
using System.Net;
using NSI.REST.Models;
using NSI.REST.Middleware;
using NSI.Repository.Mappers;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/CaseInfo")]
    public class CaseInfoController : Controller
    {
		ICaseInfoManipulation _caseInfoRepository { get; set; }

		public CaseInfoController(ICaseInfoManipulation caseInfoManipulation)
		{
			_caseInfoRepository = caseInfoManipulation;
		}

		// GET: api/CaseInfo
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_caseInfoRepository.GetCasesInfo());
		}

        //      // GET: api/CaseInfo/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //return Ok(_caseInfoRepository.GetCaseInfoById(id));
            return Ok();
        }

        // POST: api/CaseInfo
        [HttpPost]
        public IActionResult Post([FromBody]CaseInfoCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CaseInfoDto caseInfoDto = new CaseInfoDto()
            {
                CaseNumber = model.CaseNumber,
                CourtNumber = model.CourtNumber,
                Value = model.Value,
                Judge = model.Judge,
                Court = model.Court,
                CounterParty=model.CounterParty,
                Note = model.Note,
                 CaseCategory=model.CaseCategory,
                CustomerId=model.CustomerId,
                ClientId=model.ClientId,
                CreatedByUserId=model.CreatedByUserId
            };

            try
            {
                var caseInfo = _caseInfoRepository.CreateCaseInfo(caseInfoDto);
                if (caseInfo != null)
                    return Ok(caseInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // PUT: api/CaseInfo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
