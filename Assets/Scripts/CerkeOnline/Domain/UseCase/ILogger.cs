namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public interface ILogger
    {
        void Log(ISystemMessage message);
    }
}