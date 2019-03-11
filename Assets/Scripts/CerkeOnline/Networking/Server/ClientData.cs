using Mirror;
using Azarashi.Utilities.Text;

namespace Azarashi.CerkeOnline.Networking.Server
{
    /// <summary>
    /// サーバが保持するクライアントに関する情報.
    /// </summary>
    public class ClientData
    {
        public readonly string id;
        public readonly NetworkConnection connection;

        public ClientData(NetworkConnection connection)
        {
            this.connection = connection;
            id = new RandomStringGenerator().Generate(128);
        }
    }
}