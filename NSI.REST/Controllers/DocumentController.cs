﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSI.BLL.Interfaces;
using NSI.DC.DocumentRepository;
using NSI.DC.Exceptions;

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
        [HttpGet("byCase/{id}")]
        public IActionResult GetByCaseId(int id)
        {
            try
            {
                return Ok(DocumentManipulation.GetDocumentsByCase(id));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw new NSIException(ex.Message, DC.Exceptions.Enums.Level.Error, DC.Exceptions.Enums.ErrorType.InvalidParameter);
            }
        }
        [HttpGet]
        [Route("case/{caseId}")]
        public IActionResult GetNumberOfDocumentsByCase(int caseId)
        {
            try
            {
                return Ok(DocumentManipulation.GetNumberOfDocumentsByCase(caseId));

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw new NSIException(ex.Message, DC.Exceptions.Enums.Level.Error, DC.Exceptions.Enums.ErrorType.InvalidParameter);
            }
        }

        [HttpGet]
        [Route("category")]
        public IActionResult GetDocumentCategories()
        {
            try
            {
                return Ok(DocumentManipulation.GetDocumentCategories());

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw new NSIException(ex.Message, DC.Exceptions.Enums.Level.Error, DC.Exceptions.Enums.ErrorType.InvalidParameter);
            }
        }

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
                throw new NSIException(ex.Message, DC.Exceptions.Enums.Level.Error, DC.Exceptions.Enums.ErrorType.InvalidParameter);
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
                throw new NSIException(ex.Message, DC.Exceptions.Enums.Level.Error, DC.Exceptions.Enums.ErrorType.InvalidParameter);
            }
        }

        // GET api/Documents/5
        [HttpGet("history/{id}")]
        public IActionResult GetDocumentHistory(int id)
        {
            try
            {
                return Ok(DocumentManipulation.GetDocumentHistoryByDocumentId(id));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw new NSIException(ex.Message, DC.Exceptions.Enums.Level.Error, DC.Exceptions.Enums.ErrorType.InvalidParameter);
            }
        }

        //POST /api/Documents/paging
        [HttpPost]
        [Route("paging")]
        public IActionResult GetDocumentsByPage([FromBody]DocumentsPagingQueryModel queryDto)
        {
            try
            {
                return Ok(DocumentManipulation.GetDocumentsByPage(queryDto));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw new NSIException(ex.Message, DC.Exceptions.Enums.Level.Error, DC.Exceptions.Enums.ErrorType.InvalidParameter);
            }
        }

        

        // POST api/Documents/upload
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files.FirstOrDefault();
                var path = await DocumentManipulation.UploadFileAsync(file);
                return Ok(Path.Combine("localhost:59738", path));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        // POST api/Documents
        [HttpPost]
        public IActionResult Post([FromBody]CreateDocumentDto document)
        {
            try
            {
                var doc = DocumentManipulation.SaveDocument(document);
                return Ok(doc);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw new NSIException(ex.Message, DC.Exceptions.Enums.Level.Error, DC.Exceptions.Enums.ErrorType.InvalidParameter);
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
                throw new NSIException(ex.Message, DC.Exceptions.Enums.Level.Error, DC.Exceptions.Enums.ErrorType.InvalidParameter);
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
                throw new NSIException(ex.Message, DC.Exceptions.Enums.Level.Error, DC.Exceptions.Enums.ErrorType.InvalidParameter);
            }
        }
    }
}
