using BookMyShow.BuinessLogicLayer.CustomExceptions;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.BuinessLogicLayer.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookMyShow.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager _userManager;
        public UserController(UserManager userManager)
        {
            this._userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var result = await _userManager.GetUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            try
            {
                var user = await _userManager.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(UserDto user)
        {
            try
            {
                await _userManager.AddUser(user);
                return Ok("User Created Successfully");
            }
            catch (CustomException ex) { return BadRequest(ex.list); }

        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateUser(int id, UserDto user)
        {
            try
            {
                await _userManager.UpdateUser(id, user);
                return Ok("User updated successfully");
            }
            catch (CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _userManager.DeleteUser(id);
                return Ok("User deleted successfully");
            } catch(Exception ex) { return BadRequest(JsonConvert.SerializeObject(ex.InnerException)); }
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(EmailAndPasswordDto EP)
        {
            try
            {
                await _userManager.LoginUser(EP);
                return Ok("Login successfully");
            }
            catch (CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpPost("Upsert")]
        public async Task<ActionResult> CreateOrUpdate(UserDto userDto)
        {
            try
            {
                var result = await _userManager.CreateOrUpdate(userDto);
                return Ok(result);
            }
            catch (CustomException ex) { return BadRequest(ex.list); }
        }
    }
}
