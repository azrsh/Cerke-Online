using System;
using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Networking.DataStructure
{
    /// <summary>
    /// RPCによる送受信用のデータ構造.
    /// </summary>
    [Serializable]
    public struct PieceMoveData
    {
        public readonly string senderId;
        public readonly Vector2 start;
        public readonly Terminologies.PieceName piece;
        public readonly Vector2 end;
        public readonly byte numberOfstick;

        public PieceMoveData(string senderId, Vector2Int start, Terminologies.PieceName piece, Vector2Int end, byte numberOfstick = byte.MaxValue)
        {
            this.senderId = senderId;
            this.start = start;
            this.piece = piece;
            this.end = end;
            this.numberOfstick = numberOfstick;
        }
    }
}