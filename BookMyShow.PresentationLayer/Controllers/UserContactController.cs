using BookMyShow.BuinessLogicLayer.CustomExceptions;
using BookMyShow.BuinessLogicLayer.DTOs;
using BookMyShow.BuinessLogicLayer.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContactController : ControllerBase
    {
        private readonly UserContactManager _userContactManager;

        public UserContactController(UserContactManager userContactManager)
        {
            this._userContactManager = userContactManager;
        }

        [HttpGet]

        public async Task<List<UserContactDto>> GetUserContacts()
        {
            var userContacts = await _userContactManager.GetUserContacts();
            return userContacts;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserContactDto>> GetUserContactById(int id)
        {
            try
            {
                var userContact = await _userContactManager.GetUserContactById(id);
                return Ok(userContact);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost]
        public async Task<ActionResult> AddUserContact(UserContactDto userContact)
        {
            try
            {
                await _userContactManager.AddUserContact(userContact);
                return Ok("User contact added successfully");
            }
            catch (CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateUserContact(int id, UserContactDto userContact)
        {
            try
            {
                await _userContactManager.UpdateUserContact(id, userContact);
                return Ok("User contact updated successfully");
            }
            catch (CustomException ex) { return BadRequest(ex.list); }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserContact(int id)
        {
            try
            {
                await _userContactManager.DeleteUserContact(id);
                return Ok("User contact deleted successfully");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
