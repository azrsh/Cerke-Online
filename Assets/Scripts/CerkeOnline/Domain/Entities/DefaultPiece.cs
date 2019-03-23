using System.Collections.Generic;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public abstract class DefaultPiece : IPiece
    {
        public IPlayer Owner => owner;
        IPlayer owner;

        public Terminologies.PieceName PieceName => pieceName;
        readonly Terminologies.PieceName pieceName;

        public Vector2Int Position => position;
        Vector2Int position;

        public int Color => color;
        readonly int color;

        public virtual int NumberOfMoves { get { return 1; } }

        /// <summary>
        /// 初期座標を設定する.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="normalPieceMovements"></param>
        /// <param name="expansionPieceMovements"></param>
        public DefaultPiece(Vector2Int position, int color, IPlayer owner, Terminologies.PieceName pieceName)
        {
            this.pieceName = pieceName;
            this.position = position;
            this.color = color;
            this.owner = owner;
        }

        public abstract IReadOnlyList<PieceMovement> GetMoveablePosition(bool isExpanded = false);

        public bool MoveTo(Vector2Int position)
        {
            if (!IsMoveable(position)) return false;
            this.position = position;
            return true;
        }

        public bool IsMoveable(Vector2Int position)
        {
            PieceMovement pieceMovement;
            return TryToGetPieceMovement(position, out pieceMovement);
        }

        public bool TryToGetPieceMovement(Vector2Int position, out PieceMovement pieceMovement)
        {
            bool isExpanded = false;//this.position == imperialArea;
            bool isLocalPlayer = Owner == Application.GameController.Instance.Game.FirstPlayer;//仮の条件
            Vector2Int relativePosition = position - this.position;
            if (isLocalPlayer) relativePosition *= -1;                                          //逆にしたい（!isLocalPlayerのとき-1をかける）
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
            this.owner = owner;
        }

        public virtual bool PickUpFromBoard()
        {
            if (!IsPickupable()) return false;

            position = new Vector2Int(-1, -1);
            return true;
        }

        public void SetOnBoard(Vector2Int position)
        {
            if (this.position == new Vector2Int(-1, -1))
                this.position = position;
        }

        public virtual bool IsOwner(IPlayer player)
        {
            return owner == player;
        }

        public virtual bool IsPickupable()
        {
            return true;
        }
    }
}