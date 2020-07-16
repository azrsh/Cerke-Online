using System;
using System.Collections.Generic;
using System.Diagnostics;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.ActualAction
{
    internal class Capturer
    {
        readonly PositionArrayAccessor<IPiece> pieces;

        public Capturer(PositionArrayAccessor<IPiece> pieces)
        {
            this.pieces = pieces;
        }

        bool IsCapturable(IPlayer player, IPiece movingPiece, IPiece targetPiece)
        {
            bool canMovingPieceTakePiece = movingPiece.CanTakePiece();
            bool isPieceCapturable = targetPiece.IsCapturable();
            bool isSameOwner = targetPiece.Owner == player; 
            return canMovingPieceTakePiece && isPieceCapturable && !isSameOwner;
        }

        public CaptureResult CapturePiece(IPlayer player, IPiece capturer, PublicDataType.IntegerVector2 targetPosition)
        {   
            IPiece targetPiece = pieces.Read(targetPosition);
            if (targetPiece == null) 
                return new CaptureResult(true, capturer, null, null, targetPosition);

            if (!IsCapturable(player ,capturer, targetPiece) || !targetPiece.CaptureFromBoard())
                return new CaptureResult(false, capturer, null, null, targetPosition);
            
            IPlayer formerOwner = targetPiece.Owner;
            targetPiece.SetOwner(player);
            pieces.Write(targetPosition, null);
            return new CaptureResult(true, capturer, targetPiece, formerOwner, targetPosition);
        }
    }

    internal struct CaptureResult : IEquatable<CaptureResult>
    {
        public bool IsSuccess { get; }
        public IPiece Capturer { get; }
        public IPiece Captured { get; }
        public IPlayer FormerOwner{get;}
        public IntegerVector2 From { get; }

        public CaptureResult(bool isSuccess, IPiece capturer, IPiece captured, IPlayer formerOwner, IntegerVector2 from)
        {
            IsSuccess = isSuccess;
            Capturer = capturer;
            Captured = captured;
            FormerOwner = formerOwner;
            From = from;
        }

        public bool Equals(CaptureResult other)
        {
            return this.Capturer == other.Capturer && this.Captured == other.Captured && this.From == other.From;
        }

        public override bool Equals(Object other)
        {
            throw new InvalidOperationException();
        }

        public override int GetHashCode()
        {
            int hashCode = 1080449093;
            hashCode = hashCode * -1521134295 + EqualityComparer<IPiece>.Default.GetHashCode(Capturer);
            hashCode = hashCode * -1521134295 + EqualityComparer<IPiece>.Default.GetHashCode(Captured);
            hashCode = hashCode * -1521134295 + From.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(CaptureResult left, CaptureResult right) => left.Equals(right);
        public static bool operator !=(CaptureResult left, CaptureResult right) => !left.Equals(right);
    }
}