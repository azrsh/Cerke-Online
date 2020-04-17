namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IGameInstanceFactory
    {
        IGame CreateInstance(Terminologies.Encampment firstPlayerEncampment);
    }
}