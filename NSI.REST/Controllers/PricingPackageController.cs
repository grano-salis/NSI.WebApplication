using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Interfaces;
using NSI.DC.PricingPackageRepository;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PricingPackageController:Controller
    {
        private readonly IPricingPackageManipulation _pricingPackageManipulation;
        public PricingPackageController(IPricingPackageManipulation pricingPackageManipulation)
        {
            _pricingPackageManipulation = pricingPackageManipulation;
        }

        [HttpGet]
        public IEnumerable<PricingPackageDto> GetPricingPackages()
        {
            return _pricingPackageManipulation.GetPricingPackages();
        }

        [HttpGet("{id}")]
        public IActionResult GetPricingPackage(int id){
            var pricingPackage = _pricingPackageManipulation.GetPricingPackage(id);
            if(pricingPackage==null){
                return BadRequest(id);
            }
            return Ok(pricingPackage);
        }

        [HttpGet("Active")]
        public IEnumerable<PricingPackageDto> GetActivePackages()
        {
            return _pricingPackageManipulation.GetActivePricingPackages();
        }

        [HttpPost]
        public IActionResult Post([FromBody] PricingPackageDto pricingPackage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Ovdje bi vjerovatno trebalo povuci pricingpackage radi eventualne provjere
                    pricingPackage.DateCreated = new DateTime();
                    pricingPackage.DateModified = pricingPackage.DateCreated;
                    var result = _pricingPackageManipulation.SavePricingPackage(pricingPackage);
                    if (result != null) return Ok(result);
                }
                else return BadRequest(pricingPackage);
            }
            catch (Exception e)
            {

            }
            return BadRequest();
        }
    }
}
