using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSI.BLL.Interfaces;
using NSI.DC.DocumentRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/Documents")]
    public class DocumentController : Controller
    {
        private IDocumentManipulation DocumentManipulation { get; }
        private ILogger<DocumentController> Logger { get; }

        public DocumentController(IDocumentManipulation documentManipulation, ILogger<DocumentController> logger)
        {
            DocumentManipulation = documentManipulation;
            Logger = logger;
        }

        // GET: api/Documents
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(DocumentManipulation.GetAllDocuments());
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        //POST /api/Documents/paging
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
                Logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        // GET api/Documents/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(DocumentManipulation.GetDocumentById(id));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        // POST api/Documents/upload
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            try
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
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        // POST api/Documents
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
                Logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        // PUT api/Documents/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]DocumentDto documentDto)
        {
            try
            {
                return Ok(DocumentManipulation.EditDocument(id, documentDto));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        // DELETE api/Documents/3
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(DocumentManipulation.DeleteDocument(id));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
