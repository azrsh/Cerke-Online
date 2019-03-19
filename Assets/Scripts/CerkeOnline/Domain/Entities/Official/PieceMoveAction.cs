using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Azarashi.Utilities.Collections;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.Official
{
    public class PieceMoveAction
    {
        readonly Vector2ArrayAccessor<IPiece> pieces;
        readonly Vector2ArrayAccessor<FieldEffect> columns;
        readonly IValueInputProvider<int> valueProvider;
        readonly IReadOnlyList<Vector2Int> relativePath;
        readonly IReadOnlyList<Vector2Int> worldPath;
        readonly PieceMovement pieceMovement;
        readonly Action<PieceMoveResult> callback;
        readonly Action onPiecesChanged;
        bool surmounted = false;

        readonly Vector2Int startPosition;

        public PieceMoveAction(Vector2Int startPosition, Vector2Int endPosition, Vector2ArrayAccessor<IPiece> pieces, Vector2ArrayAccessor<FieldEffect> columns, 
            IValueInputProvider<int> valueProvider, PieceMovement pieceMovement, Action<PieceMoveResult> callback, Action onPiecesChanged)
        {
            this.pieces = pieces;
            this.columns = columns;
            this.valueProvider = valueProvider;

            this.startPosition = startPosition;
            bool isLocalPlayer = pieces.Read(startPosition).Owner == Application.GameController.Instance.Game.FirstPlayer;
            Vector2Int relativePosition = (endPosition - startPosition) * (isLocalPlayer ? -1 : 1);
            this.relativePath = pieceMovement.GetPath(relativePosition);
            this.worldPath = relativePath.Select(value => startPosition + value * (isLocalPlayer ? -1 : 1)).ToArray();

            this.pieceMovement = pieceMovement;
            this.callback = callback;
            this.onPiecesChanged = onPiecesChanged;
        }

        IPiece PickUpPiece(IPiece movingPiece,Vector2Int endWorldPosition)
        {
            IPiece originalPiece = pieces.Read(endWorldPosition);     //命名が分かりにくい. 行先にある駒.
            if (originalPiece == null || originalPiece.Owner == movingPiece.Owner)
                return null;
            
            IPiece gottenPiece = originalPiece;
            if(!gottenPiece.PickUpFromBoard()) return null;
            gottenPiece.SetOwner(movingPiece.Owner);
            pieces.Write(endWorldPosition, null);
            return gottenPiece;
        }

        void ConfirmPiecePosition(IPiece movingPiece, Vector2Int startWorldPosition, Vector2Int endWorldPosition)
        {
             movingPiece.MoveTo(endWorldPosition);

            //SetPieceを利用すべき？
            pieces.Write(endWorldPosition, movingPiece);
            pieces.Write(startWorldPosition, null);

            onPiecesChanged();
        }

        void LastMove(IPiece movingPiece, Vector2Int startWorldPosition, Vector2Int endWorldPosition)
        {
            //移動先の駒を取る
            IPiece gottenPiece = PickUpPiece(movingPiece, endWorldPosition);
            ConfirmPiecePosition(movingPiece, startWorldPosition, endWorldPosition);
            callback(new PieceMoveResult(true, true, gottenPiece));
        }

        public void StartMove()
        {
            Move(true, startPosition, 0);
        }

        void Move(bool condition, Vector2Int start, int index)
        {
            IPiece movingPiece = pieces.Read(start);

            //再帰終了処理
            if (!condition)
            {
                if (index > 1)
                    LastMove(movingPiece, start, worldPath[index - 2]);
                if (index == 1)
                    callback(new PieceMoveResult(true, true, null));
                return;
            }
            if (index >= relativePath.Count)
            {
                LastMove(movingPiece, start, worldPath[index - 1]);
                return;
            }
            
            IPiece piece = pieces.Read(worldPath[index]);

            //入水判定の必要があるか
            bool isInWater = (index > 0 && IsInWater(worldPath[index - 1])) || (index == 0 && IsInWater(start));
            bool isIntoWater = IsInWater(worldPath[index]);
            if (!isInWater && isIntoWater)
            {
                if (index > 0) ConfirmPiecePosition(movingPiece, start, worldPath[index - 1]);
                valueProvider.RequestValue(value => Move(value >= 3, movingPiece.Position, ++index));
                return;
            }

            //PieceMovementが踏み越えに対応しているか
            if (piece != null && !surmounted && pieceMovement.surmountable)
            {
                Debug.Log("Surmount");
                surmounted = true;

                if (index > 0) ConfirmPiecePosition(movingPiece, start, worldPath[index - 1]);
                valueProvider.RequestValue(value => Move(value >= 3, worldPath[index], ++index));
                return;
            }

            if (piece != null)
            {
                if (piece.IsPickupable())
                {
                    LastMove(movingPiece, start, worldPath[index]);
                    return;
                }

                //取ることが出ない駒が移動ルート上にある場合は移動失敗として終了する
                callback(new PieceMoveResult(isSuccess: false, isTurnEnd : false, gottenPiece : null));
                return;
            }

            Move(true, start, ++index);
        }

        bool IsInWater(Vector2Int position)
        {
            return columns.Read(position) == FieldEffect.Tammua || columns.Read(position) == FieldEffect.Tanzo;
        }
    }
}