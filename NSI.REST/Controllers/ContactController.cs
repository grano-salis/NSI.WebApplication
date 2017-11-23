using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using NSI.BLL;
using NSI.DC.ContactRepository;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/Contacts")]
    public class ContactController : Controller
    {
        IContactManipulation _contactRepository { get; set; }

        public ContactController()
        {
            _contactRepository = new ContactManipulation();
        }


        [HttpGet]
        public IEnumerable<ContactDto> Get()
        {
            return _contactRepository.GetContacts();

        }
        
        [HttpGet("{id}", Name = "Get")]
        public ContactDto Get(int id)
        {
            return _contactRepository.GetContactById(id);
        }
        
        //implement after html
        [HttpPost]
        public void Post([FromBody]string value)
        {

        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}