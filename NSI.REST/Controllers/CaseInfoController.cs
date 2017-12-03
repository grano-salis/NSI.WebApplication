using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/CaseInfo")]
    public class CaseInfoController : Controller
    {
		ICaseInfoManipulation _caseInfoManipulation { get; set; }

		public CaseInfoController(ICaseInfoManipulation caseInfoManipulation)
		{
			_caseInfoManipulation = caseInfoManipulation;
		}

		// GET: api/CaseInfo
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_caseInfoManipulation.GetCasesInfo());
		}

		//      // GET: api/CaseInfo/5
		//[HttpGet("{id}", Name = "Get")]
		//public IActionResult Get(int id)
		//{
		//	//return Ok(_caseInfoManipulation.GetCaseInfoById(id));
		//	return Ok();
		//}

		// POST: api/CaseInfo
		[HttpPost]
        public void Post([FromBody]string value)
        {
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
