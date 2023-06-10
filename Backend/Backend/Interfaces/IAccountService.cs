using Backend.Dto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Backend.Interfaces
{
    public interface IAccountService
    {
        void VerifyMerchant(string merchantId);
        List<AccountDataDto> GetMerchants();
        List<AccountDataDto> GetShoppers();
        AccountLoginDto UpdateAccount(AccountLoginDto accountLoginDto);
        bool IsMerchantVerified(string email);
        string UploadImage(IFormFile imageFile, string email);
        void BlockMerchant(string merchantId);
        AccountDataDto GetUserProfile(string email);

    }
}
