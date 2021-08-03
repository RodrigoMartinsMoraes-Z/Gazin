using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

using Project.Domain.Context;
using Project.Domain.Developers;

using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Get all devs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDevelopers(int skip = 0, int take = 10)
        {
            return Ok(_context.Developers.Skip(skip).Take(take).ToList());
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
        public IActionResult GetDevelopers(string name, string hobby, Sex? sex, int skip = 0, int take = 10)
        {
            var devs = _context.Developers.Skip(skip).Take(take);

            devs = devs.Where(d => d.Name.Contains(name));
            devs = devs.Where(d => d.Hobby.Contains(hobby));

            if (sex != null)
                devs = devs.Where(d => d.Sex == sex);

            if (devs.Count() < 1)
                return NotFound();

            return Ok(devs);
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
        public IActionResult NewDeveloper(Developer developer)
        {
            try
            {
                _context.Add(developer);
                _context.SaveChanges();

                return Created(new Uri("api/developers"), developer);
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
        [HttpPut]
        public IActionResult UpdateDeveloper([FromBody] Developer developer)
        {
            try
            {
                _context.Update(developer);
                _context.SaveChanges();

                return Created(new Uri("api/developers"), developer);
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
    }
}
