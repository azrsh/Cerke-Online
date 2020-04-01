using System;
using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Networking.DataStructure
{
    /// <summary>
    /// RPCによる送受信用のデータ構造.
    /// </summary>
    [Serializable]
    public struct PieceMoveData
    {
        public readonly string senderId;
        public readonly int startX;
        public readonly int startY;
        public readonly Terminologies.PieceName piece;
        public readonly int endX;
        public readonly int endY;
        public readonly byte numberOfstick;

        public PieceMoveData(string senderId, IntegerVector2 start, Terminologies.PieceName piece, IntegerVector2 end, byte numberOfstick = byte.MaxValue)
        {
            this.senderId = senderId;
            this.startX = start.x;
            this.startY = start.y;
            this.piece = piece;
            this.endX = end.x;
            this.endY = end.y;
            this.numberOfstick = numberOfstick;
        }
    }
}