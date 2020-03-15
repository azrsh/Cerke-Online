
namespace Azarashi.CerkeOnline.Domain.Entities
{
    //quarterとかの方がいいかも？
    public interface ISeason
    {
        string Name { get; }
        Terminologies.Season Season { get; }
    }
}