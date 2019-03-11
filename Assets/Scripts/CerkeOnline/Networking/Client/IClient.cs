namespace Azarashi.CerkeOnline.Networking.Client
{
    public interface IClient
    {
        string ID { get; }
        void OnConected();
    }
}