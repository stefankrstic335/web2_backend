namespace Backend.Interfaces
{
    public interface IEmailService
    {
        void SendEmailBlocked(string emailTo);
        void SendEmailVerified(string emailTo);

    }
}
