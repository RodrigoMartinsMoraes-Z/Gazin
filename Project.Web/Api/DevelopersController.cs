using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

using Project.Domain.Context;
using Project.Domain.Developers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Project.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly IDataBaseContext _context;

        public DevelopersController(IDataBaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get devs with filters
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hobby"></param>
        /// <param name="sex"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDevelopers(Sex? sex, string name, string hobby, int skip = 0, int take = 10)
        {
            IQueryable<Developer> devs = _context.Developers.Skip(skip).Take(take);

            if (IsValidString(name))
                devs = devs.Where(d => d.Name.Contains(name));

            if (IsValidString(hobby))
                devs = devs.Where(d => d.Hobby.Contains(hobby));

            if (sex != null)
                devs = devs.Where(d => d.Sex == sex);

            return devs.Any() ? Ok(devs) : NotFound();
        }

        /// <summary>
        /// Get a dev
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("{id}")]
        public IActionResult GetDeveloper(int id)
        {
            Developer dev = _context.Developers.Find(id);

            if (dev != null)
                return Ok(dev);

            return NotFound();
        }

        /// <summary>
        /// New Dev
        /// </summary>
        /// <param name="developer"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Developer), 201)]
        public IActionResult NewDeveloper(Developer developer)
        {
            try
            {
                _context.Add(developer);
                _context.SaveChanges();

                return new ObjectResult(developer) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception e)
            {
                return BadRequest("Exception: " + e);
            }
        }

        /// <summary>
        /// Update Dev
        /// </summary>
        /// <param name="developer"></param>
        /// <returns></returns>
        [HttpPut, Route("{id}")]
        public IActionResult UpdateDeveloper(int id, [FromBody] Developer developer)
        {
            try
            {
                developer.Id = id;
                _context.Update(developer);
                _context.SaveChanges();

                return new ObjectResult(developer) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception e)
            {
                return BadRequest("Exception: " + e);
            }
        }

        /// <summary>
        /// Delete the dev
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("{id}")]
        public IActionResult DeleteDeveloper(int id)
        {
            try
            {
                var dev = _context.Developers.Find(id);

                _context.Developers.Remove(dev);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest("Exception: " + e);
            }
        }

        private static bool IsValidString(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                return false;

            return true;
        }
    }
}
