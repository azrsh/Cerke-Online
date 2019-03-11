using System;
using System.Collections.Generic;
using Azarashi.CerkeOnline.Networking.DataStructure;

namespace Azarashi.CerkeOnline.Networking.Client
{
    public interface IServerDelegate
    {
        void PostMoveData(PieceMoveData moveData);
        IObservable<PieceMoveData> OnRecievedMoveData { get; }
        void RequestRetransmission(Action<PieceMoveData> callback);
        void RequestAllLogs(Action<IReadOnlyList<PieceMoveData>> callback);
    }
}