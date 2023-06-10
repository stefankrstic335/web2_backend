using Backend.Dto;
using Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Backend.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AccountLoginDto login)
        {
            var token = _loginService.Login(login);
            if (token != null)
            {
                if(token == string.Empty)
                {
                    return BadRequest("Wrong password!");

                }
                return Ok(token);
            }
            else
            {
                return BadRequest("Wrong email!");
            }

        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] AccountDataDto regModel)
        {
            try
            {
                _loginService.Register(regModel);
                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
