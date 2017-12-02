using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using NSI.DC.AddressRepository;
using NSI.REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/address")]
    public class AddressController : Controller
    {
        IAddressManipulation AddressRepository { get; set; }

        public AddressController(IAddressManipulation addressManipulation)
        {
            this.AddressRepository = addressManipulation;
        }


        // GET: api/address
        [HttpGet]
        public IActionResult GetAddreses()
        {
            return Ok(AddressRepository.GetAddreses());
        }

        // GET: api/address/1
        [HttpGet("{id}", Name = "GetAddress")]
        public IActionResult GetAddress(int id)
        {
            return Ok(AddressRepository.GetAddressById(id));
        }

        // POST: api/address
        [HttpPost]
        public IActionResult Post([FromBody]AddressCreateModels model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AddressDto addressDto = new AddressDto()
            {
                Address1 = model.Address1,
                Address2 = model.Address2,
                City = model.City,
                ZipCode = model.ZipCode,
                AddressTypeId = model.AddressTypeId,
                CreatedByUserId = model.CreatedByUserId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            try
            {
                var address = AddressRepository.CreateAddress(addressDto);
                if (address != null)
                    return Ok(address);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
                //return BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}
