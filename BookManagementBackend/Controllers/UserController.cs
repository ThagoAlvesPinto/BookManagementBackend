using BookManagementBackend.Classes;
using BookManagementBackend.Domain.Interfaces.Services;
using BookManagementBackend.Domain.Models;
using BookManagementBackend.Domain.Models.Requests;
using BookManagementBackend.Domain.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookManagementBackend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ITokenService _tokenService;

        public UserController(IUserService service, ITokenService tokenService)
        {
            _service = service;
            _tokenService = tokenService;
        }

        [HttpPost("Login"), AllowAnonymous]
        public async Task<ActionResult<APIResponse<LoginResponse>>> Login(LoginRequest loginRequest)
        {
            ServiceResult<Users> loginResult = await _service.Login(loginRequest.Email, loginRequest.Senha);

            if (!loginResult.Sucesso || loginResult.Resultado is null)
                return BadRequest(new APIResponse(false, loginResult.Mensagem));

            return Ok(new APIResponse<string>(_tokenService.GeraToken(loginResult.Resultado), mensagem: "Login realizado com sucesso!"));
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
