using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/addreses")]
    public class AddressController : Controller
    {
        IAddressManipulation AddressRepository { get; set; }

        public AddressController(IAddressManipulation addressManipulation)
        {
            this.AddressRepository = addressManipulation;
        }


        // GET: api/address
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(AddressRepository.GetAddreses());
        }
    }
}
