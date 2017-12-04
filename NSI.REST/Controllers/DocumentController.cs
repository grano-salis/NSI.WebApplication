using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSI.BLL.DocumentRepository;
using NSI.REST.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NSI.REST.Controllers
{
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        IDocumentManipulation _documentRepository { get; set; }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [Route("paging")]
        public DocumentsPagingResultModel GetDocumentsByPage(Models.DocumentsPagingQueryModel queryDto)
        {
            try
            {
                return _documentRepository.GetDocumentsByPage(queryDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
