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
    public class CarsController : Controller
    {

        ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: api/cars
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _carService.GetCarDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // GET api/cars/5
        [HttpGet("{carId}")]
        public IActionResult GetById(int carId)
        {
            var result = _carService.GetById(carId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // GET api/GetByBrandId/5
        [HttpGet("getByBrand/{brandId}")]
        public IActionResult GetByBrandId(int brandId)
        {
            var result = _carService.GetCarsByBrandId(brandId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // GET api/GetByColorId/5
        [HttpGet("GetByColor/{colorId}")]
        public IActionResult GetByColorId(int colorId)
        {
            var result = _carService.GetCarsByColorId(colorId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // GET api/GetByColorId/5
        [HttpGet("GetByColorAndBrand")]
        public IActionResult GetByColorIdAndBrandId(int colorId,int brandId)
        {
            var result = _carService.GetCarsByColorIdAndBrandId(colorId, brandId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // POST api/cars
        [HttpPost]
        public IActionResult Post([FromBody] Car car)
        {
            var result = _carService.Add(car);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        // DELETE api/cars/5
        [HttpPut("{carId}")]
        public IActionResult Update([FromBody] Car car)
        {
            var result = _carService.Update(car);
            if (result.Success)
            {
                return Ok(result);
            }
            return Ok(result.Message);
        }


        // DELETE api/cars/5
        [HttpDelete]
        public IActionResult Delete([FromBody] Car car)
        {
            var result = _carService.Delete(car);
            if (result.Success)
            {
                return Ok(result);
            }
            return Ok(result.Message);
        }
    }
}
