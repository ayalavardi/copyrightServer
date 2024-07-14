using Microsoft.AspNetCore.Mvc;
using Dto.models;
using Bl.Interfaces;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;

        public UserController(IUser userService)
        {
            _userService = userService;
        }

        [HttpGet("Login")]
        public async Task<ActionResult<User>> Login([FromQuery(Name = "email")] string email, [FromQuery(Name = "password")] string password)
        {
            try
            {
                if (email == null || password == null)
                    return BadRequest("Invalid firstName/lastName/email/password!");
                User userFound = await _userService.LogInAsync(email, password);
                if (userFound == null)
                {
                    return NotFound("User not found");
                }
                return Ok(userFound);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("LoginGoogle")]
        public async Task<ActionResult<User>> LoginGoogle([FromQuery(Name = "email")] string email, [FromQuery(Name = "name")] string name)
        {
            try
            {
                if (email == null || name == null)
                    return BadRequest("Invalid firstName/lastName/email/password!");
                User userFound = await _userService.LogInGoogleAsync(email, name);
                if (userFound == null)
                {
                    return NotFound("User not found");
                }
                return Ok(userFound);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateAsync([FromBody] User user)
        {
            try
            {
                User createdUser = await _userService.CreateAsync(user);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("email is already"))
                {
                    return Conflict("User with this email already exists.");
                }
                if(ex.Message.Contains("role is not exist"))
                {
                    return BadRequest("Invalid role!");
                }
                throw new Exception(ex.Message);
            }
           
        }

        [HttpDelete("DeleteByEmail")]
        public async Task<ActionResult<User>> DeleteByEmail([FromQuery(Name = "email")] string email)
        {
            if (email == null) return BadRequest("Invalid email");
            bool deleted = await _userService.DeleteByEmailAsync(email);
            if (deleted)
                return Content(deleted.ToString());
            else
                return NotFound("User not found, The email not exist!");
        }

        [HttpDelete("DeleteById")]
        public async Task<ActionResult<User>> Delete([FromQuery(Name = "id")] int id)
        {
            try
            {
                if (id < 0) return BadRequest("Invalid id!");
                bool deleted = await _userService.DeleteByIdAsync(id);
                if (deleted)
                    return Content(deleted.ToString());
                else
                    return NotFound("User not found, The id not exist");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet("GetById")]
        public async Task<ActionResult<User>> GetById([FromQuery(Name = "id")] int id)
        {
            try
            {
                return await _userService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("user does not exist in DB"))
                    return NotFound(ex.Message);
                else throw new Exception(ex.Message);
            }


        }

        [HttpGet("GetByEmail")]
        public async Task<ActionResult<User>> GetByEmail([FromQuery(Name = "email")] string email)
        {
            try
            {
                return await _userService.GetByEmailAsync(email);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("user does not exist in DB"))
                    return NotFound(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> ReadAllAsync()
        {
            try
            {
                return await _userService.ReadAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut]
        public async Task<bool> UpdatePassword([FromBody] UserUpdateRequest request)
        {
            return await _userService.UpdatePassword(request.Email, request.Password);
        }
    }
    public class UserUpdateRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
