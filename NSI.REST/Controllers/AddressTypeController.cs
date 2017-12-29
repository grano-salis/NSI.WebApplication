using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using NSI.DC.AddressTypeRepository;
using NSI.REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/addresstype")]
    public class AddressTypeController : Controller
    {
        IAddressTypeManipulation _addressTypeManipulation { get; set; }

        public AddressTypeController(IAddressTypeManipulation addressTypeManipulation)
        {
            _addressTypeManipulation = addressTypeManipulation;
        }

        // GET: api/addresstype
        [HttpGet]
        public IActionResult GetAddressTypes()
        {
            return Ok(_addressTypeManipulation.GetAddressTypes());
        }

        // GET: api/addresstypes/1
        [HttpGet("{id}", Name = "GetAddressType")]
        public IActionResult GetAddressType(int id)
        {
            return Ok(_addressTypeManipulation.GetAddressTypeById(id));
        }

        // POST: api/addresstype
        [HttpPost]
        public IActionResult PostAddressType([FromBody]AddressTypeCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AddressTypeDto addressTypeDto = new AddressTypeDto()
            {
                AddressTypeName = model.AddressTypeName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            try
            {
                var addressType = _addressTypeManipulation.CreateAddressType(addressTypeDto);
                if (addressType != null)
                    return Ok(addressType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
                //return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // DELETE: api/addresstype/1
        [HttpDelete("{id}")]
        public IActionResult DeleteAddressType(int id)
        {
            try
            {
                if (_addressTypeManipulation.DeleteAddressTypeById(id))
                {
                    return NoContent();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/addresstype/1
        [HttpPut("{id}")]
        public IActionResult PutAddressType(int id, [FromBody]AddressTypeEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AddressTypeDto addressTypeDto = new AddressTypeDto()
            {
                AddressTypeName = model.AddressTypeName,
                CreatedDate = model.CreatedDate,
                ModifiedDate = model.ModifiedDate
            };

            try
            {

                if (_addressTypeManipulation.EditAddressType(id, addressTypeDto)) return Ok();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }
    }
}
