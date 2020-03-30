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
        public readonly IntegerVector2 start;
        public readonly Terminologies.PieceName piece;
        public readonly IntegerVector2 end;
        public readonly byte numberOfstick;

        public PieceMoveData(string senderId, IntegerVector2 start, Terminologies.PieceName piece, IntegerVector2 end, byte numberOfstick = byte.MaxValue)
        {
            this.senderId = senderId;
            this.start = start;
            this.piece = piece;
            this.end = end;
            this.numberOfstick = numberOfstick;
        }
    }
}