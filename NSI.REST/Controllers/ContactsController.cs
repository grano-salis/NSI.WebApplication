using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSI.DC.ContactsRepository;

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
        public IActionResult Get()
        {
            return Ok(contactsRepository.GetContacts());
        }


        // GET: api/contacts/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(contactsRepository.GetContactById(id));
        }


        // POST: api/contacts
        [HttpPost]
        public IActionResult Post([FromBody]ContactDto model)
        {
            Console.Write(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               
               var contact = contactsRepository.CreateContact(model);
                if (contact != null)
                    return Ok(contact);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
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
                var contact = contactsRepository.EditContact(id, model);
                if (contact)
                    return Ok(contact);
                else return NoContent();

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

