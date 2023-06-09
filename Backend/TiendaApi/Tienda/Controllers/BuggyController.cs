﻿using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Tienda.Errors;

namespace Tienda.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly TiendaContext _context;

        public BuggyController(TiendaContext context)
        {
            _context = context;
        }


        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _context.Productos.Find(42);
            if (thing == null) return NotFound(new ApiResponse(404)); 

            return Ok();

        }
        
         [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _context.Productos.Find(42);
            var thinToReturn = thing.ToString();

            return Ok();

        }

       
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));

        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return BadRequest();

        }
    }
}
