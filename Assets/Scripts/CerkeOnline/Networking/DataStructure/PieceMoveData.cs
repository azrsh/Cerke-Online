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
        public readonly IntVector2 start;
        public readonly Terminologies.PieceName piece;
        public readonly IntVector2 end;
        public readonly byte numberOfstick;

        public PieceMoveData(string senderId, IntVector2 start, Terminologies.PieceName piece, IntVector2 end, byte numberOfstick = byte.MaxValue)
        {
            this.senderId = senderId;
            this.start = start;
            this.piece = piece;
            this.end = end;
            this.numberOfstick = numberOfstick;
        }
    }
}