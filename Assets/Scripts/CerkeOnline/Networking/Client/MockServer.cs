using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Azarashi.Utilities.Text;
using Azarashi.CerkeOnline.Networking.DataStructure;

namespace Azarashi.CerkeOnline.Networking.Client
{
    public class MockServer : IServerDelegate
    {
        Queue<PieceMoveData> moveDataQueue = new Queue<PieceMoveData>();
        public IObservable<PieceMoveData> OnRecievedMoveData => onRecievedMoveData;
        readonly Subject<PieceMoveData> onRecievedMoveData = new Subject<PieceMoveData>();
        public readonly IClient client;

        public MockServer()
        {
            client = null;

            //RandomStringGenerator generator = new RandomStringGenerator();
            /*string id1 = string.Empty, id2 = string.Empty;
            int count = 0;
            while(id1 == id2)
            {
                if (count > 1000)
                    throw new Exception("ClientIDの発行に失敗しました.");
                id1 = generator.Generate(128);
                id2 = generator.Generate(128);
                count++;
            }*/
            //client.PostClientId(generator.Generate(128));
            
            client.OnConected();
        }

        public void PostMoveData(PieceMoveData moveData)
        {
            //if(moveData.GetHashCode() != hashCode);
            Debug.Log("Post MoveData.");
            moveDataQueue.Enqueue(moveData);
            onRecievedMoveData.OnNext(moveData);
        }

        public void RequestRetransmission(Action<PieceMoveData> callback)
        {
            Debug.Log("Request Retransmission.");
            callback?.Invoke(moveDataQueue.Peek());
        }

        public void RequestAllLogs(Action<IReadOnlyList<PieceMoveData>> callback)
        {
            Debug.Log("Request All Logs.");
            callback?.Invoke(moveDataQueue.ToArray());
        }
    }
}