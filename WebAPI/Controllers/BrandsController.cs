using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class BrandsController : Controller
    {
        IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _brandService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        // GET api/brands/5
        [HttpGet("{brandId}")]
        public IActionResult GetById(int brandId)
        {
            var result = _brandService.GetById(brandId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        // POST api/brands
        [HttpPost]
        public IActionResult Post([FromBody] Brand brand)
        {
            var result = _brandService.Add(brand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }


        // DELETE api/brands/5
        [HttpPut("{brandId}")]
        public IActionResult Update([FromBody] Brand brand)
        {
            var result = _brandService.Update(brand);
            if (result.Success)
            {
                return Ok(result);
            }
            return Ok(result.Message);
        }


        // DELETE api/brands/5
        [HttpDelete]
        public IActionResult Delete([FromBody] Brand brand)
        {
            var result = _brandService.Delete(brand);
            if (result.Success)
            {
                return Ok(result);
            }
            return Ok(result.Message);
        }
    }
}
