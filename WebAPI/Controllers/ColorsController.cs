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
    public class ColorsController : Controller
    {
        IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _colorService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        // GET api/colors/5
        [HttpGet("{colorId}")]
        public IActionResult GetById(int colorId)
        {
            var result = _colorService.GetById(colorId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        // POST api/colors
        [HttpPost]
        public IActionResult Post([FromBody] Color color)
        {
            var result = _colorService.Add(color);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }


        // DELETE api/colors/5
        [HttpPut("{colorId}")]
        public IActionResult Update([FromBody] Color color)
        {
            var result = _colorService.Update(color);
            if (result.Success)
            {
                return Ok(result);
            }
            return Ok(result.Message);
        }


        // DELETE api/colors/5
        [HttpDelete]
        public IActionResult Delete([FromBody] Color color)
        {
            var result = _colorService.Delete(color);
            if (result.Success)
            {
                return Ok(result);
            }
            return Ok(result.Message);
        }
    }
}
