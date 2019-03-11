using System.Collections.Generic;
using Mirror;
using Azarashi.Utilities.Text;
using Azarashi.CerkeOnline.Networking.DataStructure;

namespace Azarashi.CerkeOnline.Networking.Server
{
    public class ServerContainer : IReadOnlyServerContainer
    {
        readonly List<PieceMoveData> pieceMoveLogs = new List<PieceMoveData>();
        public IReadOnlyList<PieceMoveData> PieceMoveLogs => pieceMoveLogs;

        readonly Dictionary<string, NetworkIdentity> identityDictionary = new Dictionary<string, NetworkIdentity>(2);
        public IReadOnlyDictionary<string, NetworkIdentity> IdentityDictionary => identityDictionary;

        readonly RandomStringGenerator stringGenerator = new RandomStringGenerator();

        public string ResisterClient(NetworkIdentity identity)
        {
            string id = stringGenerator.Generate(64);
            identityDictionary.Add(id, identity);
            return id;
        }

        public void AddPieceMoveLogs(PieceMoveData moveData)
        {
            pieceMoveLogs.Add(moveData);
        }
    }
}