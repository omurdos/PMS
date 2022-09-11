using API.DTOs;
using API.Helpers;
using AutoMapper;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IJWTGenerator _jwt;
        public UsersController(DBContext context, IMapper mapper, UserManager<User> userManager, IJWTGenerator jwt)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _jwt = jwt;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            return Ok(_userManager.Users.ToList());
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == dto.PhoneNumber);
                    if (user == null)
                    {
                        //return NotFound($"Requested resource with {dto.PhoneNumber} don't exists, please register or try again");
                        return NotFound(
                              new
                              {
                                  Message = "Incorrect phone number or password"
                              });
                    }
                    var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
                    if (result == PasswordVerificationResult.Success)
                    {
                        //Generate token and build successful user response
                        var token = await _jwt.GenerateToken(user);
                        var userDTO = _mapper.Map<UserDTO>(user);
                        userDTO.Token = token;
                        return Ok(userDTO);
                    }
                    return NotFound(
                        new
                        {
                            Message = "Incorrect phone number or password"
                        });
                }
                return BadRequest(new
                {
                    Message = "Please provide both phone number and password"
                });
            }
            catch (Exception)
            {
                throw;
                //return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
