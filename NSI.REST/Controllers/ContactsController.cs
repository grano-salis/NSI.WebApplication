using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSI.DC.ContactsRepository;
using Microsoft.EntityFrameworkCore.Internal;
using System.Text.RegularExpressions;
using NSI.DC.AddressRepository;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/contacts")]
    public class ContactsController : Controller
    {
        BLL.Interfaces.IContactsManipulation contactsRepository { get; set; }

        public ContactsController(BLL.Interfaces.IContactsManipulation contactsManipulation)
        {
            this.contactsRepository = contactsManipulation;
        }

        // GET: api/contacts
        [HttpGet]
        public IActionResult Get(int pageSize, int pageNumber, String searchString, String searchColumn, String sortOrder, int caseId)
        {
            return Ok(contactsRepository.GetContacts(pageSize, pageNumber, searchString, searchColumn, sortOrder, caseId));
        }


        // GET: api/contacts/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(contactsRepository.GetContactById(id));
        }

        // GET: api/contacts/forcase/5
        [HttpGet("/forcase/{caseId}")]
        public IActionResult GetByCase(int caseId)
        {
            return Ok(contactsRepository.GetContactsForCase(caseId));
        }


        // POST: api/contacts
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody]ContactDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {


                if (model != null)
                {
                    if (contactsRepository.ValidationContact(model) == "")
                    {
                        var contact = contactsRepository.CreateContact(model, id);
                        return Ok(contact);
                    }
                    else
                        throw new Exception(contactsRepository.ValidationContact(model));

                }

                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //return NoContent();
        }

        // PUT: api/contacts/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ContactDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (contactsRepository.ValidationContact(model) == "")
                {
                    var contact = contactsRepository.EditContact(id, model);
                    if (contact)
                    {
                        return Ok(contact);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    throw new Exception(contactsRepository.ValidationContact(model));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/contacts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (contactsRepository.DeleteContactById(id))
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

