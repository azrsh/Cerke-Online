
namespace Azarashi.CerkeOnline.Domain.Entities
{
    //quarterとかの方がいいかも？
    public interface ISeason : IReadOnlySeason
    {
        void Continue();

        void Quit();
    }
}