using Backend.Dto;
using Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountService _userService;
        private readonly IEmailService _emailService;

        public UserController(IAccountService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpGet("getMerchants")]
        [Authorize(Roles = "admin")]
        public IActionResult GetMerchants()
        {
            return Ok(_userService.GetMerchants());
        }

        [HttpGet("getShoppers")]
        [Authorize(Roles = "admin")]
        public IActionResult GetShoppers()
        {
            return Ok(_userService.GetShoppers());
        }


        [HttpGet("getUserData")]
        [Authorize(Roles = "admin, merchant, shopper")]
        public IActionResult GetUserData(string email)
        {
            return Ok(_userService.GetUserProfile(email));
        }

        [HttpPost("blockMerchant")]
        [Authorize(Roles = "admin")]
        public IActionResult BlockMerchant(string merchantId)
        {
            try
            {
                _userService.BlockMerchant(merchantId);
                _emailService.SendEmailBlocked(merchantId);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("verifyMerchant")]
        [Authorize(Roles = "admin")]
        public IActionResult VerifyMerchant(string merchantId)
        {
            try
            {
                _userService.VerifyMerchant(merchantId);
                _emailService.SendEmailVerified(merchantId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("uploadImage"), DisableRequestSizeLimit]
        [Authorize(Roles = "admin, merchant, shopper")]
        public async Task<IActionResult> UploadImage([FromQuery] string email)
        {
            var formCollection =  await Request.ReadFormAsync();
            var file = formCollection.Files.First();
            return Ok(_userService.UploadImage(file, email));
        }

        [HttpPost("updateAccount")]
        [Authorize(Roles = "admin, merchant, shopper")]
        public IActionResult UpdateAccount(AccountDataDto account)
        {
            return Ok(_userService.UpdateAccount(account));
        }
    }
}
