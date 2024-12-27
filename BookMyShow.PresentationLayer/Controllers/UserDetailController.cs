using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.BuinessLogicLayer.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        private readonly UserDetailManager _userDetailManager;

        public UserDetailController(UserDetailManager userDetailManager)
        {
            this._userDetailManager = userDetailManager;
        }

        [HttpGet]
        public async Task<List<UserDetailDto>> GetUserDetails()
        {
            return await _userDetailManager.GetUserDetails();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailDto>> GetUserDetailById(int id)
        {
            var userDetail = await _userDetailManager.GetUserDetailById(id);
            if (userDetail == null)
            {
                return NotFound();
            }
            return Ok(userDetail);
        }

        [HttpPost]
        public async Task<ActionResult> AddUserDetail(UserDetailDto userDetail)
        {
            if (userDetail == null)
            {
                return Content("Please provide information");
            }
            await _userDetailManager.AddUserDetail(userDetail);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserDetail(int id, UserDetailDto userDetail)
        {
            var isExist = await _userDetailManager.GetUserDetailById(id);
            if (isExist == null)
            {
                return NotFound();
            }
            await _userDetailManager.UpdateUserDetail(id, userDetail);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserDetail(int id)
        {
            var userDetail = await _userDetailManager.GetUserDetailById(id);
            if (userDetail == null)
            {
                return NotFound();
            }
            await _userDetailManager.DeleteUserDetail(id);
            return Ok();
        }
    }
}
