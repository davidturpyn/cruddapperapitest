using cruddapperapitest.Contracts;
using cruddapperapitest.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cruddapperapitest.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository) => this.userRepository = userRepository;

        [HttpGet]
        public async Task<IActionResult> getDataUser()
        {
            var user = await userRepository.getDataUser();

            return Ok(user);
        }

        [HttpGet("{id}", Name = "getUser")]
        public async Task<IActionResult> getUser(int id)
        {
            var user = await userRepository.getUserById(id);
            if (user is null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> setDataUser([FromBody] UserForCreationDto user)
        {
            var userCreated = await userRepository.setDataUser(user);
            return CreatedAtRoute("getUser", new { id = userCreated.userid }, userCreated);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateDataUser(int id, [FromBody] UserForUpdateDto user)
        {
            var getUser = await userRepository.getUserById(id);
            if (getUser is null)
                return NotFound();

            await userRepository.updateDataUser(id, user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delDataUser(int id)
        {
            var getUser = await userRepository.getUserById(id);
            if (getUser is null)
                return NotFound();

            await userRepository.delDataUser(id);
            return NoContent();
        }
    }
}
