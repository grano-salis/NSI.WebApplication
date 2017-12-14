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
    [Produces("application/json")]
    [Route("api/Documents")]
    public class DocumentController : Controller
    {
        private IDocumentManipulation DocumentManipulation { get; }

        public DocumentController(IDocumentManipulation documentManipulation)
        {
            DocumentManipulation = documentManipulation;
        }

        // GET: api/Documents
        [HttpGet]
        public IActionResult Get()
        {
            var documents = DocumentManipulation.GetAllDocuments();
            return Ok(documents);
        }

        [HttpPost]
        [Route("paging")]
        public IActionResult GetDocumentsByPage(DocumentsPagingQueryModel queryDto)
        {
            try
            {
                return Ok(DocumentManipulation.GetDocumentsByPage(queryDto));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(DocumentManipulation.GetDocumentById(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // POST api/values
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            var size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length <= 0) continue;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            DocumentManipulation.UploadFile(files, filePath);
            return Ok(new { count = files.Count, size, filePath });
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(DocumentDto document)
        {
            try
            {
                DocumentManipulation.SaveDocument(document);
                return Ok(document);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]DocumentDto documentDto)
        {
            return Ok(DocumentManipulation.EditDocument(documentDto));
        }

        // DELETE api/values/5
        [HttpDelete("{id}/{currentUserId}")]
        public IActionResult Delete(int id, int currentUserId)
        {
            try
            {
                return Ok(DocumentManipulation.DeleteDocument(id));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            } 
        }
    }
}
