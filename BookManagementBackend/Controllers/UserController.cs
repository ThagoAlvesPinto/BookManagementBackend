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
        private readonly IUsersService _service;
        private readonly ITokenService _tokenService;

        public UserController(IUsersService service, ITokenService tokenService)
        {
            _service = service;
            _tokenService = tokenService;
        }

        [HttpPost("Login"), AllowAnonymous]
        public async Task<ActionResult<APIResponse<LoginResponse>>> Login(LoginRequest loginRequest)
        {
            ServiceResult<Users> loginResult = await _service.Login(loginRequest.Email ?? "", loginRequest.Password ?? "");

            if (loginResult.ExceptionGenerated)
                return StatusCode(500, new APIResponse(false, loginResult.Message));

            if (!loginResult.Success || loginResult.Data is null)
                return BadRequest(new APIResponse(false, loginResult.Message));

            string token = _tokenService.GenerateToken(loginResult.Data);

            LoginResponse loginResponse = new(loginResult.Data, token);

            return Ok(new APIResponse<LoginResponse>(loginResponse));
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<APIResponse<List<UserResponse>>>> GetUsers()
        {
            ServiceResult<List<UserResponse>> result = await _service.GetAllUsers();

            if (result.ExceptionGenerated)
                return StatusCode(500, (APIResponse)result);

            if (!result.Success)
                return BadRequest((APIResponse)result);

            return Ok((APIResponse<List<UserResponse>>)result);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse<UserResponse>>> Get(int id)
        {
            ServiceResult<UserResponse> result = await _service.GetUser(id);

            if (result.ExceptionGenerated)
                return StatusCode(500, (APIResponse)result);

            if (!result.Success)
                return BadRequest((APIResponse)result);

            return Ok((APIResponse<UserResponse>)result);
        }

        // POST api/User
        [HttpPost]
        public async Task<ActionResult<APIResponse>> AddUser([FromBody] AddUserRequest request)
        {
            ServiceResult result = await _service.AddUser(request);

            if (result.ExceptionGenerated)
                return StatusCode(500, (APIResponse)result);

            if (!result.Success)
                return BadRequest((APIResponse)result);

            return Ok((APIResponse)result);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse>> UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            ServiceResult result = await _service.UpdateUser(request, id);

            if (result.ExceptionGenerated)
                return StatusCode(500, (APIResponse)result);

            if (!result.Success)
                return BadRequest((APIResponse)result);

            return Ok((APIResponse)result);
        }

        // PUT api/<UserController>/UpdatePassword/5
        [HttpPut("UpdatePassword/{id}")]
        public async Task<ActionResult<APIResponse>> UpdatePassword(int id, [FromBody] UpdatePasswordRequest request)
        {
            ServiceResult result = await _service.UpdatePassword(request, id);

            if (result.ExceptionGenerated)
                return StatusCode(500, (APIResponse)result);

            if (!result.Success)
                return BadRequest((APIResponse)result);

            return Ok((APIResponse)result);
        }

        // PUT api/<UserController>/DeactivateUser/5
        [HttpPut("DeactivateUser/{id}")]
        public async Task<ActionResult<APIResponse>> DeactivateUser(int id)
        {
            ServiceResult result = await _service.DeactivateUser(id);

            if (result.ExceptionGenerated)
                return StatusCode(500, (APIResponse)result);

            if (!result.Success)
                return BadRequest((APIResponse)result);

            return Ok((APIResponse)result);
        }
    }
}
