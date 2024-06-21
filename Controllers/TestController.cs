using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/test")]
    [ApiController]

    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
             var dummyData = new List<object>
            {
                new { Id = 1, Name = "Item1", Description = "This is item 1" },
                new { Id = 2, Name = "Item2", Description = "This is item 2" },
                new { Id = 3, Name = "Item3", Description = "This is item 3" }
            };

            {
                return Ok(dummyData);
            }
        }

    }
}