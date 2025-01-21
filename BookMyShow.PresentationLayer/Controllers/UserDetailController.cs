using BookMyShow.BuinessLogicLayer.CustomExceptions;
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
            try
            {
                var userDetail = await _userDetailManager.GetUserDetailById(id);
                return Ok(userDetail);
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost]
        public async Task<ActionResult> AddUserDetail(UserDetailDtoRequest userDetail)
        {
            try
            {
                await _userDetailManager.AddUserDetail(userDetail);
                return Ok("User details added successfully");
            } catch (CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateUserDetail(int id, UserDetailDtoRequest userDetail)
        {
            try
            {
                await _userDetailManager.UpdateUserDetail(id, userDetail);
                return Ok("Updated user details successfully");
            } catch (CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserDetail(int id)
        {
            try
            {
                await _userDetailManager.DeleteUserDetail(id);
                return Ok("Deleted user details successfully");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
