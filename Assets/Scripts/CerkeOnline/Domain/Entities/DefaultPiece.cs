using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public abstract class DefaultPiece : IPiece
    {
        //駒の情報
        public IPlayer Owner { get; private set; }
        public Terminologies.PieceName PieceName { get; }
        public Vector2Int Position { get; private set; }
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
        public DefaultPiece(Vector2Int position, Terminologies.PieceColor color, IPlayer owner, Terminologies.PieceName pieceName, IExpandingMoveFieldChecker fieldChecker)
        {
            this.PieceName = pieceName;
            this.Position = position;
            this.Color = color;
            this.Owner = owner;
            this.fieldChecker = fieldChecker;
        }

        public abstract IReadOnlyList<PieceMovement> GetMoveablePosition(bool isExpanded = false);

        public bool MoveTo(Vector2Int position, bool isForceMove = false)
        {
            if (!isForceMove && !IsMoveable(position)) return false;
            this.Position = position;
            return true;
        }

        public bool IsMoveable(Vector2Int worldPosition)
        {
            PieceMovement pieceMovement;
            return TryToGetPieceMovement(worldPosition, out pieceMovement);
        }

        public bool TryToGetPieceMovement(Vector2Int worldPosition, out PieceMovement pieceMovement)
        {
            var relativePosition = ConvertWorldPositionToRelativePosition(worldPosition);
            return TryToGetPieceMovementByRelativePosition(relativePosition, out pieceMovement);
        }

        public bool TryToGetPieceMovementByRelativePosition(Vector2Int relativePosition, out PieceMovement pieceMovement)
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

            Position = new Vector2Int(-1, -1);
            return true;
        }

        public void SetOnBoard(Vector2Int position)
        {
            if (this.Position == new Vector2Int(-1, -1))
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


        Vector2Int ConvertWorldPositionToRelativePosition(Vector2Int worldPosition)
        {
            bool isFrontPlayer = Owner != null && Owner.Encampment == Terminologies.Encampment.Front;//仮の条件
            Vector2Int relativePosition = worldPosition - this.Position;
            if (isFrontPlayer) relativePosition *= -1;                                          //逆にしたい（!isLocalPlayerのとき-1をかける）
            return relativePosition;
        }

        Vector2Int ConvertRelativePositionToWorldPosition(Vector2Int relativePosition)
        {
            bool isFrontPlayer = Owner != null && Owner.Encampment == Terminologies.Encampment.Front;//仮の条件
            if (isFrontPlayer) relativePosition *= -1;                                          //逆にしたい（!isLocalPlayerのとき-1をかける）
            Vector2Int worldPosition = relativePosition + this.Position;
            return worldPosition;
        }
    }
}