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
        public int Color { get; }
        public virtual int NumberOfMoves { get { return 1; } }

        //外部へのアクセス
        readonly IExpandingMoveFieldChecker fieldChecker;

        /// <summary>
        /// 初期座標を設定する.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="normalPieceMovements"></param>
        /// <param name="expansionPieceMovements"></param>
        public DefaultPiece(Vector2Int position, int color, IPlayer owner, Terminologies.PieceName pieceName, IExpandingMoveFieldChecker fieldChecker)
        {
            this.PieceName = pieceName;
            this.Position = position;
            this.Color = color;
            this.Owner = owner;
            this.fieldChecker = fieldChecker;
        }

        public abstract IReadOnlyList<PieceMovement> GetMoveablePosition(bool isExpanded = false);

        public bool MoveTo(Vector2Int position)
        {
            if (!IsMoveable(position)) return false;
            this.Position = position;
            return true;
        }

        public bool IsMoveable(Vector2Int position)
        {
            PieceMovement pieceMovement;
            return TryToGetPieceMovement(position, out pieceMovement);
        }

        public bool TryToGetPieceMovement(Vector2Int targetPosition, out PieceMovement pieceMovement)
        {
            bool isExpanded = fieldChecker != null && fieldChecker.IsExpandedMoveField(this.Position);
            bool isFrontPlayer = Owner != null && Owner.Encampment == Terminologies.Encampment.Front;//仮の条件
            Vector2Int relativePosition = targetPosition - this.Position;
            if (isFrontPlayer) relativePosition *= -1;                                          //逆にしたい（!isLocalPlayerのとき-1をかける）
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

        public virtual bool PickUpFromBoard()
        {
            if (!IsPickupable()) return false;

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

        public virtual bool IsPickupable()
        {
            return true;
        }

        public virtual bool CanLittuaWithoutJudge() => false;
    }
}