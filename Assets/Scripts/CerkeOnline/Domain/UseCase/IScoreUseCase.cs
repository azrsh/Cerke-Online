namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public interface IScoreUseCase
    {
        int Score { get; }
        void LogHandDifference();
    }
}