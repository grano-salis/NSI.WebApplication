using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using NSI.REST.Models;
using NSI.DC.DocumentRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NSI.REST.Controllers
{
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        private readonly IDocumentManipulation _documentManipulation;

        public DocumentController(IDocumentManipulation documentManipulation)
        {
            _documentManipulation = documentManipulation;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            
            return new[] { "value1", "value2" };
        }

        [HttpPost]
        [Route("paging")]
        public PagingResultModel<DocumentDto> GetDocumentsByPage(DocumentsPagingQueryModel queryDto)
        {
            try
            {
                return _documentManipulation.GetDocumentsByPage(queryDto);
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
        public void Post([FromBody]DocumentDto documentDto)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]DocumentDto documentDto)
        {
            _documentManipulation.EditDocument(id, documentDto);
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            var size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath });
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                _documentManipulation.DeleteDocument(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            } 
        }
    }
}
