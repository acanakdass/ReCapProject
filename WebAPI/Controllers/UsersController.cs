﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        //// GET api/users/5
        //[HttpGet("{userId}")]
        //public IActionResult GetById(int userId)
        //{
        //    var result = _userService.GetById(userId);
        //    if (result.Success)
        //    {
        //        return Ok(result.Data);
        //    }
        //    return BadRequest(result.Message);
        //}

        // POST api/users
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            var result = _userService.Add(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }


        //// DELETE api/users/5
        //[HttpPut("{userId}")]
        //public IActionResult Update([FromBody] User user)
        //{
        //    var result = _userService.Update(user);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return Ok(result.Message);
        //}


        //// DELETE api/users/5
        //[HttpDelete]
        //public IActionResult Delete([FromBody] User user)
        //{
        //    var result = _userService.Delete(user);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return Ok(result.Message);
        //}
    }
}
