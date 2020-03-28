using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    internal abstract class DefaultPiece : IPiece
    {
        //駒の情報
        public IPlayer Owner { get; private set; }
        public Terminologies.PieceName PieceName { get; }
        public PublicDataType.IntegerVector2 Position { get; private set; }
        public Terminologies.PieceColor Color { get; }
        public virtual int NumberOfMoves { get { return 1; } }

        //外部へのアクセス
        readonly IExpandingMoveFieldChecker fieldChecker;

        /// <summary>
        /// 初期座標を設定する.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="normalPieceMovements"></param>
        /// <param name="expansionPieceMovements"></param>
        internal DefaultPiece(PublicDataType.IntegerVector2 position, Terminologies.PieceColor color, IPlayer owner, Terminologies.PieceName pieceName, IExpandingMoveFieldChecker fieldChecker)
        {
            this.PieceName = pieceName;
            this.Position = position;
            this.Color = color;
            this.Owner = owner;
            this.fieldChecker = fieldChecker;
        }

        public abstract IReadOnlyList<PieceMovement> GetMoveablePosition(bool isExpanded = false);

        public bool MoveTo(PublicDataType.IntegerVector2 position, bool isForceMove = false)
        {
            if (!isForceMove && !IsMoveable(position)) return false;
            this.Position = position;
            return true;
        }

        public bool IsMoveable(PublicDataType.IntegerVector2 worldPosition)
        {
            PieceMovement pieceMovement;
            return TryToGetPieceMovement(worldPosition, out pieceMovement);
        }

        public bool TryToGetPieceMovement(PublicDataType.IntegerVector2 worldPosition, out PieceMovement pieceMovement)
        {
            var relativePosition = ConvertWorldPositionToRelativePosition(worldPosition);
            return TryToGetPieceMovementByRelativePosition(relativePosition, out pieceMovement);
        }

        public bool TryToGetPieceMovementByRelativePosition(PublicDataType.IntegerVector2 relativePosition, out PieceMovement pieceMovement)
        {
            bool isExpanded = fieldChecker != null && fieldChecker.IsExpandedMoveField(this.Position);
            foreach (PieceMovement moveable in GetMoveablePosition(isExpanded))
            {
                if (moveable.IsMoveable(relativePosition))
                {
                    pieceMovement = moveable;
                    return true;
                }
            }

            pieceMovement = PieceMovement.Default;
            return false;
        }

        public virtual void SetOwner(IPlayer owner)
        {
            this.Owner = owner;
        }

        public virtual bool CaptureFromBoard()
        {
            if (!IsCapturable()) return false;

            Position = new PublicDataType.IntegerVector2(-1, -1);
            return true;
        }

        public void SetOnBoard(PublicDataType.IntegerVector2 position)
        {
            if (this.Position == new PublicDataType.IntegerVector2(-1, -1))
                this.Position = position;
        }

        public virtual bool IsOwner(IPlayer player)
        {
            return Owner == player;
        }

        public virtual bool IsCapturable()
        {
            return true;
        }

        public virtual bool CanLittuaWithoutJudge() => false;

        public virtual bool CanTakePiece() => true;


        PublicDataType.IntegerVector2 ConvertWorldPositionToRelativePosition(PublicDataType.IntegerVector2 worldPosition)
        {
            bool isFrontPlayer = Owner != null && Owner.Encampment == Terminologies.Encampment.Front;//仮の条件
            PublicDataType.IntegerVector2 relativePosition = worldPosition - this.Position;
            if (isFrontPlayer) relativePosition *= -1;                                          //逆にしたい（!isLocalPlayerのとき-1をかける）
            return relativePosition;
        }

        PublicDataType.IntegerVector2 ConvertRelativePositionToWorldPosition(PublicDataType.IntegerVector2 relativePosition)
        {
            bool isFrontPlayer = Owner != null && Owner.Encampment == Terminologies.Encampment.Front;//仮の条件
            if (isFrontPlayer) relativePosition *= -1;                                          //逆にしたい（!isLocalPlayerのとき-1をかける）
            PublicDataType.IntegerVector2 worldPosition = relativePosition + this.Position;
            return worldPosition;
        }
    }
}