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
            var userContact = await _userContactManager.GetUserContactById(id);
            if (userContact == null)
            {
                return NotFound();
            }
            return Ok(userContact);
        }

        [HttpPost]
        public async Task<ActionResult> AddUserContact(UserContactDto userContact)
        {
            if (userContact == null)
            {
                return Content("Please provide information");
            }
            await _userContactManager.AddUserContact(userContact);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserContact(int id, UserContactDto userContact)
        {
            var isExist = await _userContactManager.GetUserContactById(id);
            if (isExist == null)
            {
                return NotFound();
            }
            await _userContactManager.UpdateUserContact(id, userContact);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserContact(int id)
        {
            var userContact = await _userContactManager.GetUserContactById(id);
            if (userContact == null)
            {
                return NotFound();
            }
            await _userContactManager.DeleteUserContact(id);
            return Ok();
        }
    }
}
