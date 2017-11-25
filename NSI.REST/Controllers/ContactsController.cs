using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/contacts")]
    public class ContactsController : Controller
    {
        BLL.Interfaces.IContactsManipulation ContactsRepository { get; set; }

        public ContactsController(BLL.Interfaces.IContactsManipulation contactsManipulation)
        {
            this.ContactsRepository = contactsManipulation;
        }

        // GET: api/contacts
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(ContactsRepository.GetContacts());
        }
    }
}

