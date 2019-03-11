using System.Collections.Generic;
using Mirror;
using Azarashi.CerkeOnline.Networking.DataStructure;

namespace Azarashi.CerkeOnline.Networking.Server
{
    public interface IReadOnlyServerContainer
    {
        IReadOnlyList<PieceMoveData> PieceMoveLogs { get; }
        IReadOnlyDictionary<string, NetworkIdentity> IdentityDictionary { get; }
    }
}