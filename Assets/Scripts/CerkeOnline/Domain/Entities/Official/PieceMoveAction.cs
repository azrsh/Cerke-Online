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
        readonly IPlayer player;
        readonly Vector2ArrayAccessor<IPiece> pieces;
        readonly IFieldEffectChecker fieldEffectChecker;
        readonly IValueInputProvider<int> valueProvider;
        readonly IReadOnlyList<Vector2Int> relativePath;
        readonly IReadOnlyList<Vector2Int> worldPath;
        readonly PieceMovement pieceMovement;
        readonly Action<PieceMoveResult> callback;
        readonly Action onPiecesChanged;
        readonly bool isTurnEnd;
        bool surmounted = false;

        readonly Vector2Int startPosition;

        public PieceMoveAction(IPlayer player,Vector2Int startPosition, Vector2Int endPosition, Vector2ArrayAccessor<IPiece> pieces, IFieldEffectChecker fieldEffectChecker, 
            IValueInputProvider<int> valueProvider, PieceMovement pieceMovement, Action<PieceMoveResult> callback, Action onPiecesChanged, bool isTurnEnd)
        {
            this.player = player ?? throw new ArgumentNullException("駒を操作するプレイヤーを指定してください.");
            this.pieces = pieces ?? throw new ArgumentNullException("盤面の情報を入力してください.");
            this.fieldEffectChecker = fieldEffectChecker ?? throw new ArgumentNullException("フィールド効果の情報を入力してください.");
            this.valueProvider = valueProvider ?? throw new ArgumentNullException("投げ棒の値を提供するインスタンスを指定してください.");

            this.startPosition = startPosition;
            bool isFrontPlayersPiece = pieces.Read(startPosition).Owner != null && pieces.Read(startPosition).Owner.Encampment == Encampment.Front;
            Vector2Int relativePosition = (endPosition - startPosition) * (isFrontPlayersPiece ? -1 : 1);
            this.relativePath = pieceMovement.GetPath(relativePosition) ?? throw new ArgumentException("移動不可能な移動先が指定されました.");
            this.worldPath = relativePath.Select(value => startPosition + value * (isFrontPlayersPiece ? -1 : 1)).ToArray();

            this.pieceMovement = pieceMovement;
            this.callback = callback;
            this.onPiecesChanged = onPiecesChanged;
            this.isTurnEnd = isTurnEnd;
        }

        IPiece PickUpPiece(IPiece movingPiece,Vector2Int endWorldPosition)
        {
            IPiece originalPiece = pieces.Read(endWorldPosition);     //命名が分かりにくい. 行先にある駒.
            if (originalPiece == null || originalPiece.Owner == player)
                return null;
            
            IPiece gottenPiece = originalPiece;
            if(!gottenPiece.PickUpFromBoard()) return null;
            gottenPiece.SetOwner(player);
            pieces.Write(endWorldPosition, null);
            return gottenPiece;
        }

        void ConfirmPiecePosition(IPiece movingPiece, Vector2Int endWorldPosition, bool isForceMove = false)
        {
            Vector2Int startWorldPosition = movingPiece.Position;
            movingPiece.MoveTo(endWorldPosition, isForceMove);

            //この順で書きまないと現在いる座標と同じ座標をendWorldPositionに指定されたとき盤上から駒の判定がなくなる
            pieces.Write(startWorldPosition, null);
            pieces.Write(endWorldPosition, movingPiece);
            
            onPiecesChanged();
        }

        void LastMove(IPiece movingPiece, Vector2Int endWorldPosition)
        {
            //移動先の駒を取る
            IPiece gottenPiece = PickUpPiece(movingPiece, endWorldPosition);
            ConfirmPiecePosition(movingPiece, endWorldPosition);
            callback(new PieceMoveResult(true, isTurnEnd, gottenPiece));
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
                    LastMove(movingPiece, worldPath[index - 2]);
                if (index == 1)
                    callback(new PieceMoveResult(true, isTurnEnd, null));
                return;
            }
            if (index >= relativePath.Count)
            {
                LastMove(movingPiece, worldPath[index - 1]);
                return;
            }
            
            IPiece piece = pieces.Read(worldPath[index]);

            //入水判定の必要があるか
            bool isInWater = (index > 0 && fieldEffectChecker.IsInTammua(worldPath[index - 1])) || (index == 0 && fieldEffectChecker.IsInTammua(start));
            bool isIntoWater = fieldEffectChecker.IsInTammua(worldPath[index]);
            bool canLittuaWithoutJudge = movingPiece.CanLittuaWithoutJudge();
            if (!isInWater && isIntoWater && !canLittuaWithoutJudge)
            {
                if (index > 0) ConfirmPiecePosition(movingPiece, worldPath[index - 1]);
                valueProvider.RequestValue(value => Move(value >= 3, movingPiece.Position, ++index));
                return;
            }

            //PieceMovementが踏み越えに対応しているか
            if (piece != null && !surmounted && pieceMovement.surmountable && index < worldPath.Count - 1)
            {
                surmounted = true;

                Vector2Int currentPosition = index > 0 ? worldPath[index - 1] : start;
                if (index > 0) ConfirmPiecePosition(movingPiece, worldPath[index - 1]);
                valueProvider.RequestValue(value => 
                {
                    const int Threshold = 3;
                    if (value < Threshold)
                    {
                        callback(new PieceMoveResult(false, true, null));
                        return;
                    }

                    //Unsafe 踏み越えられた場合のイベント通知
                    if (piece is ISurmountedObserver)
                        (piece as ISurmountedObserver).OnSurmounted.OnNext(Unit.Default);
                    ConfirmPiecePosition(movingPiece, worldPath[index + 1], isForceMove: true);
                    Move(value >= Threshold, worldPath[index + 1], index + 2);
                });
                return;
            }

            if (piece != null)
            {
                if (piece.IsPickupable())
                {
                    LastMove(movingPiece, worldPath[index]);
                    return;
                }

                //取ることが出ない駒が移動ルート上にある場合は移動失敗として終了する
                callback(new PieceMoveResult(isSuccess: false, isTurnEnd : false, gottenPiece : null));
                return;
            }

            Move(true, start, ++index);
        }
    }
}