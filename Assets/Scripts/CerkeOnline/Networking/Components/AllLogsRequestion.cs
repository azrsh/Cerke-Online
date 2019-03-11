using System;
using System.Linq;
using System.Collections.Generic;
using Mirror;
using Azarashi.CerkeOnline.Networking.DataStructure;
using Azarashi.CerkeOnline.Networking.Server;

namespace Azarashi.CerkeOnline.Networking.Components
{
    public class AllLogsRequestion : NetworkBehaviour, ICallbackRequestable<IReadOnlyList<PieceMoveData>>
    {
        //Client Only
        string clientId;
        readonly Queue<Action<IReadOnlyList<PieceMoveData>>> callbacks = new Queue<Action<IReadOnlyList<PieceMoveData>>>();

        //Server Only
        IReadOnlyServerContainer serverContainer;

        [Server]
        public void InitializeOnServer(IReadOnlyServerContainer serverContainer)
        {
            this.serverContainer = serverContainer;
        }

        [Client]
        public void InitializeOnClient(string clientId)
        {
            this.clientId = clientId;
        }

        [Client]
        public void Request(Action<IReadOnlyList<PieceMoveData>> callback)
        {
            callbacks.Enqueue(callback);
            CmdPostData(clientId);
        }

        [Command]
        void CmdPostData(string senderId)
        {
            NetworkIdentity identity;

            serverContainer.IdentityDictionary.TryGetValue(senderId, out identity);
            if (identity.connectionToClient.isConnected)
                TargetPostData(identity.connectionToClient, serverContainer.PieceMoveLogs.ToArray());
        }

        [TargetRpc]
        void TargetPostData(NetworkConnection target, PieceMoveData[] data)
        {
            callbacks.Dequeue()?.Invoke(data);
        }
    }
}