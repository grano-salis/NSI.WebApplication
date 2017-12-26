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
using NSI.Logger;
using NSI.DC.Response;

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

		// GET: api/case/info
		[HttpGet]
		public IActionResult Get()
		{
            return Ok(_caseInfoRepository.GetCaseInfos());
		}

        // GET: api/case/info/latest
        [HttpGet("latest")]
        public IActionResult GetLatest()
        {
            return Ok(_caseInfoRepository.GetLatestCaseInfos());
        }

        // GET: api/case/info/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var caseinfo = _mapper.Map<CaseInfoDto>(_caseInfoRepository.GetCaseInfoById(id));
            return Ok(caseinfo);
        }

        //POST: api/case/info
        [HttpPost]
        public IActionResult Post([FromBody]CaseInfoDto _caseInfoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var caseInfo = _caseInfoRepository.CreateCaseInfo(_caseInfoDto);
                if (caseInfo != null)
                    return Ok(_mapper.Map<CaseInfoDto>(caseInfo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
            return NoContent();
        }

        // PUT: api/case/info/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody]CaseInfoDto _caseInfoDto)
        {
            try
            {
                return _caseInfoRepository.EditCaseInfoById(id, _caseInfoDto);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // DELETE: api/case/info/5
        //[HttpDelete("{id}")]
        //public bool Delete(int id)
        //{
        //    try
        //    {
        //        return _caseInfoRepository.DeleteCaseInfoById(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        [HttpDelete("{id}")]
        public IActionResult DeleteCase(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _caseInfoRepository.Delete(id);
                return Ok(new NSIResponse<object>()
                {
                    Data = null,
                    Message = "Case deleted"
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
