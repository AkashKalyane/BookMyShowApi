using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.BuinessLogicLayer.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var user = await _userManager.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(UserDto user)
        {
            if (user == null)
            {
                return Content("Please provide information");
            }
            await _userManager.AddUser(user);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, UserDto user)
        {
            var isExist = await _userManager.GetUserById(id);
            if (isExist == null)
            {
                return NotFound();
            }
            await _userManager.UpdateUser(id, user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _userManager.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userManager.DeleteUser(id);
            return Ok();
        }
    }
}
