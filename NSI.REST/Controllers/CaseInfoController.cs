using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using NSI.REST.Models;
using NSI.Repository;
using NSI.DC.CaseRepository;
using System.Net.Http;
using System.Net;
using NSI.REST.Middleware;
using NSI.Repository.Mappers;
using AutoMapper;
using IkarusEntities;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/case/info")]
    public class CaseInfoController : Controller
    {
		ICaseInfoManipulation _caseInfoRepository { get; set; }
        private readonly IMapper _mapper;

        public CaseInfoController(ICaseInfoManipulation caseInfoManipulation, IMapper mapper)
		{
			_caseInfoRepository = caseInfoManipulation;
            _mapper = mapper;
		}

		// GET: api/CaseInfo
		[HttpGet]
		public IActionResult Get()
		{
            var caseinfos = _mapper.Map<IEnumerable<CaseInfoDto>>(_caseInfoRepository.GetCaseInfos());
			return Ok(caseinfos);
		}

        //      // GET: api/CaseInfo/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //return Ok(_caseInfoRepository.GetCaseInfoById(id));
            var caseinfo = _mapper.Map<CaseInfoDto>(_caseInfoRepository.GetCaseInfoDtoById(id));
            return Ok(caseinfo);
        }

        // POST: api/CaseInfo
        [HttpPost]
        public IActionResult Post([FromBody]CaseInfoDto _caseInfoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //CaseInfoDto caseInfoDto = new CaseInfoDto()
            //{
            //    CaseNumber = model.CaseNumber,
            //    CourtNumber = model.CourtNumber,
            //    Value = model.Value,
            //    Judge = model.Judge,
            //    Court = model.Court,
            //    CounterParty=model.CounterParty,
            //    Note = model.Note,
            //     CaseCategory=model.CaseCategory,
            //    CustomerId=model.CustomerId,
            //    ClientId=model.ClientId,
            //    CreatedByUserId=model.CreatedByUserId
            //};

            try
            {
                var caseInfo = _caseInfoRepository.CreateCaseInfo(_caseInfoDto);
                if (caseInfo != null)
                    return Ok(_mapper.Map<CaseInfoDto>(caseInfo));
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
