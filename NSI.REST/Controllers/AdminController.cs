using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSI.DC.AdminRepository;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/admin")]
    public class AdminController : Controller
    {
        BLL.Interfaces.IAdminManipulation adminRepository { get; set; }

        public AdminController(BLL.Interfaces.IAdminManipulation adminManipulation)
        {
            this.adminRepository = adminManipulation;
        }

        //CaseCategory

        // GET: api/admin/caseCategory
        [HttpGet("/caseCategory")]
        public IActionResult GetCases()
        {
            return Ok(adminRepository.GetCaseCategories());
        }


        // GET: api/admin/caseCategory/1
        [HttpGet("/caseCategory/{id}")]
        public IActionResult GetCase(int id)
        {
            return Ok(adminRepository.GetCaseCategoryById(id));
        }


        // POST: api/admin/caseCategory
        [HttpPost("/caseCategory")]
        public IActionResult PostCase([FromBody]CaseCategoryDto model)
        {
            Console.Write(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var caseCategory = adminRepository.CreateCaseCategory(model);
                if (caseCategory != null)
                    return Ok(caseCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // PUT: api/admin/caseCategory/1
        [HttpPut("/caseCategory/{id}")]
        public IActionResult PutCase(int id, [FromBody]CaseCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var caseCategory = adminRepository.EditCaseCategory(id, model);
                if (caseCategory)
                    return Ok(caseCategory);
                else return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/admin/caseCategory/1
        [HttpDelete("/caseCategory/{id}")]
        public IActionResult DeleteCase(int id)
        {
            try
            {

                if (adminRepository.DeleteCaseCategoryById(id))
                {
                    return Ok(id);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //ClientType

        // GET: api/admin/clientType
        [HttpGet("/clientType")]
        public IActionResult GetClients()
        {
            return Ok(adminRepository.GetClientTypes());
        }


        // GET: api/admin/clientType/1
        [HttpGet("/clientType/{id}")]
        public IActionResult GetClient(int id)
        {
            return Ok(adminRepository.GetCaseClientTypeById(id));
        }


        // POST: api/admin/clientType
        [HttpPost("/clientType")]
        public IActionResult PostClient([FromBody]ClientTypeDto model)
        {
            Console.Write(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var clientType = adminRepository.CreateClientType(model);
                if (clientType != null)
                    return Ok(clientType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // PUT: api/admin/clientType/1
        [HttpPut("/clientType/{id}")]
        public IActionResult PutClient(int id, [FromBody]ClientTypeDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var clientType = adminRepository.EditClientType(id, model);
                if (clientType)
                    return Ok(clientType);
                else return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/admin/clientType/1
        [HttpDelete("/clientType/{id}")]
        public IActionResult DeleteClient(int id)
        {
            try
            {
                if (adminRepository.DeleteClientTypeById(id))
                {
                    return Ok(id);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DocumentCategory

        // GET: api/admin/documentCategory
        [HttpGet("/documentCategory")]
        public IActionResult GetDocuments()
        {
            return Ok(adminRepository.GetDocumentCategories());
        }


        // GET: api/admin/documentCategory/1
        [HttpGet("/documentCategory/{id}")]
        public IActionResult GetDocument(int id)
        {
            return Ok(adminRepository.GetDocumentCategoryById(id));
        }


        // POST: api/admin/documentCategory
        [HttpPost("/documentCategory")]
        public IActionResult PostDocument([FromBody]DocumentCategoryDto model)
        {
            Console.Write(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var documentCategory = adminRepository.CreateDocumentCategory(model);
                if (documentCategory != null)
                    return Ok(documentCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // PUT: api/admin/documentCategory/1
        [HttpPut("/documentCategory/{id}")]
        public IActionResult PutDocument(int id, [FromBody]DocumentCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var documentCategory = adminRepository.EditDocumentCategory(id, model);
                if (documentCategory)
                    return Ok(documentCategory);
                else return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/admin/documentCategory/1
        [HttpDelete("/documentCategory/{id}")]
        public IActionResult DeleteDocument(int id)
        {
            try
            {
                if (adminRepository.DeleteDocumentCategoryById(id))
                {
                    return Ok(id);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //FileType

        // GET: api/admin/fileType
        [HttpGet("/fileType")]
        public IActionResult GetFiles()
        {
            return Ok(adminRepository.GetFileTypes());
        }


        // GET: api/admin/fileType/1
        [HttpGet("/fileType/{id}")]
        public IActionResult GetFile(int id)
        {
            return Ok(adminRepository.GetFileTypeById(id));
        }


        // POST: api/admin/fileType
        [HttpPost("/fileType")]
        public IActionResult PostFile([FromBody]FileTypeDto model)
        {
            Console.Write(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var fileType = adminRepository.CreateFileType(model);
                if (fileType != null)
                    return Ok(fileType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // PUT: api/admin/fileType/1
        [HttpPut("/fileType/{id}")]
        public IActionResult PutFile(int id, [FromBody]FileTypeDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var fileType = adminRepository.EditFileType(id, model);
                if (fileType)
                    return Ok(fileType);
                else return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/admin/fileType/1
        [HttpDelete("/fileType/{id}")]
        public IActionResult DeleteFile(int id)
        {
            try
            {
                if (adminRepository.DeleteFileTypeById(id))
                {
                    return Ok(id);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

