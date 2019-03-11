using System;
using UniRx;
using Mirror;
using Azarashi.CerkeOnline.Networking.DataStructure;

namespace Azarashi.CerkeOnline.Networking.Components
{
    public class PieceMoveDataBroadcaster : NetworkBehaviour, IBroadcastable<PieceMoveData>
    {
        public IObservable<PieceMoveData> OnRecieved => onRecieved;
        public Subject<PieceMoveData> onRecieved = new Subject<PieceMoveData>();

        [Client]
        public void Broadcast(PieceMoveData data)
        {
            CmdPostMoveData(data);
        }

        [Command]
        void CmdPostMoveData(PieceMoveData data)
        {
            RpcPostMoveData(data);
        }

        [ClientRpc]
        void RpcPostMoveData(PieceMoveData moveData)
        {
            onRecieved.OnNext(moveData);
        }
    }
}