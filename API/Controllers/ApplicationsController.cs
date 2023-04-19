using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly DBContext _context;

        public ApplicationsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/<ApplicationsController>/categories
        [HttpGet("categories")]
        public async Task<IActionResult> Categories()
        {
            try
            {
                var applications =await  _context.ApplicaionCategories.ToListAsync();
                return Ok(applications);
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        // GET: api/<ApplicationsController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string categoryId)
        {
            try
            {
                var applications = await _context.Applicaions.Include(app => app.ApplicationCategory).Where(app => app.ApplicationCategoryId == categoryId).OrderByDescending(app => app.Version).ToListAsync();
                return Ok(applications);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        // GET api/<ApplicationsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
               var app = await _context.Applicaions.FindAsync(id);
                return Ok(app);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        // POST api/<ApplicationsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ApplicationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApplicationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
