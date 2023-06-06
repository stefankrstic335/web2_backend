using Backend.Dto;

namespace Backend.Interfaces
{
    public interface ILoginService
    {
        string Login(AccountLoginDto accountLoginDto);

        void Register(AccountDataDto accountDataDto);
    }
}
