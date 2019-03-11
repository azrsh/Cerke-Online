using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Mirror;
using Azarashi.CerkeOnline.Networking.DataStructure;

namespace Azarashi.CerkeOnline.Networking.Server
{
    public class Server
    {
        //Client Member
        string clientId;
        Action<IReadOnlyList<PieceMoveData>> allLogsCallback = default;
        Action<PieceMoveData> retransmissionCallback = default;
        public IObservable<PieceMoveData> OnRecievedMoveData => onRecievedMoveData;
        readonly Subject<PieceMoveData> onRecievedMoveData = new Subject<PieceMoveData>();

        //Server Member
        readonly List<PieceMoveData> pieceMoveLogs = new List<PieceMoveData>();

        public void PostMoveData(PieceMoveData moveData)
        {
            CmdPostMoveData(moveData);
        }

        public void RequestAllLogs(Action<IReadOnlyList<PieceMoveData>> callback)
        {
            allLogsCallback = callback;
            CmdRequestAllLogs(clientId);
        }

        public void RequestRetransmission(Action<PieceMoveData> callback)
        {
            retransmissionCallback = callback;
            CmdRequestRetransmission(clientId);
        }

        [Command]
        void CmdPostMoveData(PieceMoveData data)
        {
            RpcPostMoveData(data);
        }

        [Command]
        void CmdRequestAllLogs(string senderId)
        {
            //RpcRequestAllLogs
        }

        [Command]
        void CmdRequestRetransmission(string senderId)
        {

        }

        [ClientRpc]
        void RpcPostMoveData(PieceMoveData moveData)
        {
            onRecievedMoveData.OnNext(moveData);
        }

        [TargetRpc]
        void TargetRequestAllLogs(NetworkConnection target, PieceMoveData[] data)
        {
            allLogsCallback?.Invoke(data);
            allLogsCallback = null;
        }

        [TargetRpc]
        void TargetRequestRetransmission(NetworkConnection target, PieceMoveData data)
        {
            retransmissionCallback?.Invoke(data);
            retransmissionCallback = null;
        }

        [TargetRpc]
        void TargetPostClientId(NetworkConnection target, string id)
        {
            clientId = id;
        }
    }
}