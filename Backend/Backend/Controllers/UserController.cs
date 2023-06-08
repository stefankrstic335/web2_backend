using Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountService _userService;

        public UserController(IAccountService userService)
        {
            _userService = userService;
        }

        [HttpGet("getMerchants")]

        //[Authorize(Roles = "admin")]
        public IActionResult GetMerchants()
        {
            return Ok(_userService.GetMerchants());
        }

        [HttpGet("getShoppers")]

        //[Authorize(Roles = "admin")]
        public IActionResult GetShoppers()
        {
            return Ok(_userService.GetShoppers());
        }
    }
}
