using AutoMapper;
using Backend.Database;
using Backend.Dto;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Backend.Services
{
    public class LoginService : ILoginService
    {
        private readonly IMapper _mapper;
        private readonly LocalDbContext _context;
        private readonly IConfigurationSection _secretKey;

        public LoginService(IMapper mapper, LocalDbContext context, IConfiguration config)
        {
            _mapper = mapper;
            _context = context;
            _secretKey = config.GetSection("SecretKey");
        }
        public string Login(AccountLoginDto accountLoginDto)
        {
            if (!_context.Users.Any())
            {
                return null;
            }
            User user = _context.Users.Where(x => x.Email == accountLoginDto.Email).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            if(user.AccountStatus == AccountStatus.New || user.AccountStatus == AccountStatus.Blocked)
            {
                return "User not verified";
            }

            if (BCrypt.Net.BCrypt.Verify(accountLoginDto.Password, user.Password))
            {
                List<Claim> claims = new List<Claim>();
                if (user.AccountType == AccountType.Administrator)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "admin"));
                }
                if (user.AccountType == AccountType.Shopper)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "shopper"));
                }
                if (user.AccountType == AccountType.Merchant)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "merchant"));
                }
                SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:44398",
                    claims: claims, //claimovi
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signinCredentials
                );
                return new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            }
            else
            {
                return string.Empty;
            }
        }

        public void Register(AccountDataDto accountDataDto)
        {
            if (!_context.Users.Any())
            {

                User newUser = _mapper.Map<User>(accountDataDto);
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(accountDataDto.Password);
                newUser.Id = Guid.NewGuid().ToString();

                if(newUser.AccountType == AccountType.Shopper)
                {
                    newUser.AccountStatus = AccountStatus.Verified;
                }

                _context.Users.Add(newUser);
                _context.SaveChanges();
                return;
            }

            User user = _context.Users.Where(x => x.Email == accountDataDto.Email).FirstOrDefault();
            if (user != null)
            {
                throw new Exception("This email belongs to a different account!");
            }
            else
            {
                User newUser = _mapper.Map<User>(accountDataDto);
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(accountDataDto.Password);
                newUser.Id = Guid.NewGuid().ToString();
                if (newUser.AccountType == AccountType.Shopper)
                {
                    newUser.AccountStatus = AccountStatus.Verified;
                }

                _context.Users.Add(newUser);
                _context.SaveChanges();
                return;

            }
        }
    }
}
