using System;
using System.Collections.Generic;
using UniRx;
using Mirror;
using Azarashi.Utilities.Text;
using Azarashi.CerkeOnline.Networking.DataStructure;
using Azarashi.CerkeOnline.Networking.Server;
using Azarashi.CerkeOnline.Networking.Client;

namespace Azarashi.CerkeOnline.Networking.Components
{
    public class ServerDelegateComponent : NetworkBehaviour, IServerDelegate
    {
        //Client Only
        string clientId;
        public IObservable<PieceMoveData> OnRecievedMoveData => onRecievedMoveData;
        readonly Subject<PieceMoveData> onRecievedMoveData = new Subject<PieceMoveData>();
        
        //Server Only
        readonly ServerContainer serverContainer = new ServerContainer();

        //Common
        ICallbackRequestable<IReadOnlyList<PieceMoveData>> allLogsRequestion;
        ICallbackRequestable<PieceMoveData> retransmissionRequestion;
        IBroadcastable<PieceMoveData> pieceMoveDataBroadcaster;

        private void Start()
        {
            allLogsRequestion = GetComponent<ICallbackRequestable<IReadOnlyList<PieceMoveData>>>();
            retransmissionRequestion = GetComponent<ICallbackRequestable<PieceMoveData>>();
            pieceMoveDataBroadcaster = GetComponent<IBroadcastable<PieceMoveData>>();

            if(isServer)
            {
                allLogsRequestion.InitializeOnServer(serverContainer);
                retransmissionRequestion.InitializeOnServer(serverContainer);
            }

            if(isClient)
            {
                clientId = serverContainer.ResisterClient(netIdentity);
                allLogsRequestion.InitializeOnClient(clientId);
                retransmissionRequestion.InitializeOnClient(clientId);
                pieceMoveDataBroadcaster.OnRecieved.TakeUntilDestroy(this).Subscribe(onRecievedMoveData);
            }
        }

        private void OnConnectedToServer()
        {
            CmdRequestClientId(netIdentity);
        }

        //Client Interfaces
        [Client]
        public void PostMoveData(PieceMoveData moveData)
        {
            pieceMoveDataBroadcaster.Broadcast(moveData);
        }

        [Client]
        public void RequestAllLogs(Action<IReadOnlyList<PieceMoveData>> callback)
        {
            allLogsRequestion?.Request(callback);
        }

        [Client]
        public void RequestRetransmission(Action<PieceMoveData> callback)
        {
            retransmissionRequestion?.Request(callback);
        }
        



        //Server Commands
        [Command]
        void CmdRequestClientId(NetworkIdentity identity)
        {
            string id = new RandomStringGenerator().Generate(128);
            TargetPostClientId(identity.connectionToClient, id);
        }

        //Client Rpcs
        [TargetRpc]
        void TargetPostClientId(NetworkConnection target, string id)
        {
            clientId = id;
        }
    }
}