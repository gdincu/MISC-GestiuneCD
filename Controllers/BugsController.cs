﻿using GestiuneCD.Models.Errors;
using GestiuneCD.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace GestiuneCD.Controllers
{
    /**
     */
    public class BugsController : BaseApiController
    {
        private readonly AppDbContext _context;
        /**
         */
        public BugsController(AppDbContext context)
        {
            _context = context;
        }

        /**
         * public IActionResult Index()
         */
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _context.CDs.Find(999);

            if (thing is null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok();
        }

        /**
         */
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {

            return Ok();
           
        }

        /** 
         */
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}
