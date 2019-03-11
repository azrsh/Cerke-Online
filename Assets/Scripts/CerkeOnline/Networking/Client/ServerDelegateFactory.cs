namespace Azarashi.CerkeOnline.Networking.Client
{
    public static class ServerDelegateFactory
    {
        public static IServerDelegate ConnectToServer(IClient client)
        {
            return new MockServer();
        }
    }
}