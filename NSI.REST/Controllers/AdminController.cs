﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSI.DC.AdminRepository;
using NSI.REST.Models;

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

        // GET: /caseCategory
        [HttpGet("/caseCategory")]
        public IActionResult GetCases()
        {
            return Ok(adminRepository.GetCaseCategories());
        }


        // GET: /caseCategory/1
        [HttpGet("/caseCategory/{id}")]
        public IActionResult GetCase(int id)
        {
            return Ok(adminRepository.GetCaseCategoryById(id));
        }


        // POST: /caseCategory
        [HttpPost("/caseCategory")]
        public IActionResult PostCase([FromBody]CaseCategoryCreateModel model)
        {
            Console.Write(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CaseCategoryDto caseCategoryDto = new CaseCategoryDto()
            {
                CaseCategoryName = model.CaseCategoryName,
                CustomerId = model.CustomerId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            try
            {

                var caseCategory = adminRepository.CreateCaseCategory(caseCategoryDto);
                if (caseCategory != null)
                    return Ok(caseCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // PUT: /caseCategory/1
        [HttpPut("/caseCategory/{id}")]
        public IActionResult PutCase(int id, [FromBody]CaseCategoryEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CaseCategoryDto caseCategoryDto = new CaseCategoryDto()
            {
                CaseCategoryId = model.CaseCategoryId,
                CaseCategoryName = model.CaseCategoryName,
                CustomerId = model.CustomerId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            try
            {
                var caseCategory = adminRepository.EditCaseCategory(id, caseCategoryDto);
                if (caseCategory)
                    return Ok(caseCategory);
                else return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: /caseCategory/1
        [HttpDelete("/caseCategory/{id}")]
        public IActionResult DeleteCase(int id)
        {
            try
            {
                var cases = adminRepository.GetNumberOfCasesByCaseCategory(id);
                if (cases == 0)
                {

                    if (adminRepository.DeleteCaseCategoryById(id))
                    {
                        return Ok(id);

                    }
                    return NoContent();
                }
                throw new Exception("Can't delete this case category. " + cases.ToString() + " cases belong to this case category.");

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //ClientType

        // GET: /clientType
        [HttpGet("/clientType")]
        public IActionResult GetClients()
        {
            return Ok(adminRepository.GetClientTypes());
        }


        // GET: /clientType/1
        [HttpGet("/clientType/{id}")]
        public IActionResult GetClient(int id)
        {
            return Ok(adminRepository.GetCaseClientTypeById(id));
        }


        // POST: /clientType
        [HttpPost("/clientType")]
        public IActionResult PostClient([FromBody]ClientTypeCreateModel model)
        {
            Console.Write(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ClientTypeDto clientTypeDto = new ClientTypeDto()
            {
                ClientTypeName = model.ClientTypeName,
                CustomerId = model.CustomerId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            try
            {

                var clientType = adminRepository.CreateClientType(clientTypeDto);
                if (clientType != null)
                    return Ok(clientType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // PUT: /clientType/1
        [HttpPut("/clientType/{id}")]
        public IActionResult PutClient(int id, [FromBody]ClientTypeEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ClientTypeDto clientTypeDto = new ClientTypeDto()
            {
                ClientTypeId = model.ClientTypeId,
                ClientTypeName = model.ClientTypeName,
                CustomerId = model.CustomerId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            try
            {
                var clientType = adminRepository.EditClientType(id, clientTypeDto);
                if (clientType)
                    return Ok(clientType);
                else return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: /clientType/1
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

        // GET: /documentCategory
        [HttpGet("/documentCategory")]
        public IActionResult GetDocuments()
        {
            return Ok(adminRepository.GetDocumentCategories());
        }


        // GET: /documentCategory/1
        [HttpGet("/documentCategory/{id}")]
        public IActionResult GetDocument(int id)
        {
            return Ok(adminRepository.GetDocumentCategoryById(id));
        }


        // POST: /documentCategory
        [HttpPost("/documentCategory")]
        public IActionResult PostDocument([FromBody]DocumentCategoryCreateModel model)
        {
            Console.Write(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DocumentCategoryDto documentCategoryDto = new DocumentCategoryDto()
            {
                DocumentCategoryTitle = model.DocumentCategoryTitle,
                CustomerId = model.CustomerId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            try
            {

                var documentCategory = adminRepository.CreateDocumentCategory(documentCategoryDto);
                if (documentCategory != null)
                    return Ok(documentCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // PUT: /documentCategory/1
        [HttpPut("/documentCategory/{id}")]
        public IActionResult PutDocument(int id, [FromBody]DocumentCategoryEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DocumentCategoryDto documentCategoryDto = new DocumentCategoryDto()
            {
                DocumentCategoryId = model.DocumentCategoryId,
                DocumentCategoryTitle = model.DocumentCategoryTitle,
                CustomerId = model.CustomerId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            try
            {
                var documentCategory = adminRepository.EditDocumentCategory(id, documentCategoryDto);
                if (documentCategory)
                    return Ok(documentCategory);
                else return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: /documentCategory/1
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

        // GET: /fileType
        [HttpGet("/fileType")]
        public IActionResult GetFiles()
        {
            return Ok(adminRepository.GetFileTypes());
        }


        // GET: /fileType/1
        [HttpGet("/fileType/{id}")]
        public IActionResult GetFile(int id)
        {
            return Ok(adminRepository.GetFileTypeById(id));
        }


        // POST: /fileType
        [HttpPost("/fileType")]
        public IActionResult PostFile([FromBody]FileTypeCreateModel model)
        {
            Console.Write(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            FileTypeDto fileTypeDto = new FileTypeDto()
            {
                Extension = model.Extension,
                IconPath = model.IconPath,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            try
            {

                var fileType = adminRepository.CreateFileType(fileTypeDto);
                if (fileType != null)
                    return Ok(fileType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // PUT: /fileType/1
        [HttpPut("/fileType/{id}")]
        public IActionResult PutFile(int id, [FromBody]FileTypeEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            FileTypeDto fileTypeDto = new FileTypeDto()
            {
                Extension = model.Extension,
                IconPath = model.IconPath,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            try
            {
                var fileType = adminRepository.EditFileType(id, fileTypeDto);
                if (fileType)
                    return Ok(fileType);
                else return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: /fileType/1
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

