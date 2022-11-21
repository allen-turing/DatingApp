﻿using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetString() 
        {
            return "Secret Text";
        }
        
        [HttpGet("not-found")]
        public ActionResult<AppUser> GetServerError() 
        {
            var thing = _context.Users.Find(-1);

            if(thing == null)
            {
                return NotFound();
            }

            return Ok(thing);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetNotFound()
        {
            try
            {
                var thing = _context.Users.Find(-1);

                var thingToReturn = thing.ToString();
                return thingToReturn;
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Computer Say no!");
            }   
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
           return BadRequest("This was not a good request");
        }

    }
}