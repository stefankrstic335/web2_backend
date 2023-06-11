using AutoMapper;
using Backend.Database;
using Backend.Dto;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace Backend.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly LocalDbContext _context;

        public AccountService(IMapper mapper, LocalDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        
        public List<AccountDataDto> GetMerchants()
        {
            return _mapper.Map<List<AccountDataDto>>(_context.Users.Where(x => x.AccountType == Models.AccountType.Merchant).ToList());
        }

        public List<AccountDataDto> GetShoppers()
        {
            return _mapper.Map<List<AccountDataDto>>(_context.Users.Where(x => x.AccountType == Models.AccountType.Shopper).ToList());
        }

        public AccountDataDto GetUserProfile(string email)
        {
            return _mapper.Map<AccountDataDto>(_context.Users.FirstOrDefault(x => x.Email == email));
        }

        public bool IsMerchantVerified(string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);
            if(user.AccountStatus == AccountStatus.Verified)
            {
                return true;
            }
            if(user.AccountStatus == AccountStatus.Blocked)
            {
                return false;
            }
            return false;
        }

        public string UploadImage(IFormFile imageFile, string email)
        {
            try
            {
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (imageFile.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(imageFile.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }
                    var us = _context.Users.FirstOrDefault(x => x.Email == email);
                    us.ImageUrl = dbPath;
                    _context.Users.Update(us);
                    _context.SaveChanges();
                    return dbPath;
                }
                else
                {
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }

        public void VerifyMerchant(string merchantId)
        {
            var merchant = _context.Users.Where(x => x.Email == merchantId && x.AccountType == AccountType.Merchant).FirstOrDefault(); 
            merchant.AccountStatus = AccountStatus.Verified;
            _context.SaveChanges();
        }
        public void BlockMerchant(string merchantId)
        {
            var merchant = _context.Users.Where(x => x.Email == merchantId && x.AccountType == AccountType.Merchant).FirstOrDefault();
            merchant.AccountStatus = AccountStatus.Blocked;
            _context.SaveChanges();
        }

        public AccountDataDto UpdateAccount(AccountDataDto accountLoginDto)
        {
            var us = _context.Users.AsNoTracking().FirstOrDefault(x => x.Email == accountLoginDto.Email);
            var id = us.Id;
            var accType = us.AccountType;
            var accStatus = us.AccountStatus;
            var imageUrl = us.ImageUrl;
            var password = us.Password;
            us = _mapper.Map<User>(accountLoginDto);
            us.Id = id;
            us.AccountType = accType;
            us.AccountStatus = accStatus;
            us.ImageUrl = imageUrl;
            us.Password = password;
            try
            {
                _context.Update(us);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return accountLoginDto;
        }
    }
}
