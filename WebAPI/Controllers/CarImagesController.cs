using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CarImagesController : Controller
    {

        ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{carId}")]
        public IActionResult GetImagesByCarId(int carId)
        {
            var result = _carImageService.GetImagesByCarId(carId);
            return Ok(result);
        }

        // POST api/carImages
        [HttpPost]
        public IActionResult Post([FromForm(Name =("Image"))] IFormFile file,[FromForm] CarImage carImage)
        {
            var result = _carImageService.Add(file, carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromForm(Name = ("Image"))] IFormFile file, [FromForm] CarImage carImage)
        {
            var result = _carImageService.Update(file, carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // DELETE api/values/5
        [HttpDelete("{imageId}")]
        public IActionResult Delete(int imageId)
        {
            var result = _carImageService.Delete(imageId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
