
namespace Azarashi.CerkeOnline.Domain.Entities
{
    //quarterとかの方がいいかも？
    public interface IReadOnlySeason
    {
        string Name { get; }
        Terminologies.Season Season { get; }
    }
}