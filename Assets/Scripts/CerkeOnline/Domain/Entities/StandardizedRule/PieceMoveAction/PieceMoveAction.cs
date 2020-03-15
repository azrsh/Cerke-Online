using System;
using System.Collections.Generic;
using UnityEngine;
using Azarashi.Utilities.Collections;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.DataStructure;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.ActualAction;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.AbstractAction;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction
{
    /*命名に関する注意
     以下のクラスでは, 
     ・Surmountとその派生を巫の駒越え
     ・Steppingを踏み越え
     という意味で使っています.
     */

    public class PieceMoveAction : IPieceMoveAction
    {
        readonly IPlayer player;
        readonly Vector2YXArrayAccessor<IPiece> pieces;
        readonly LinkedList<ColumnData> worldPath;
        readonly PieceMovement viaPieceMovement;
        readonly Action<PieceMoveResult> callback;
        readonly bool isTurnEnd;
        bool surmounted = false;

        readonly Vector2Int startPosition;
        readonly Vector2Int viaPosition;
        readonly Vector2Int endPosition;

        readonly Mover pieceMover;
        readonly Pickupper pickupper;
        readonly WaterEntryChecker waterEntryChecker;
        readonly MoveFinisher moveFinisher;
        readonly SurmountingChecker surmountingChecker;
        readonly SteppingChecker steppingChecker;

        public PieceMoveAction(MoveActionData moveActionData, Vector2YXArrayAccessor<IPiece> pieces, IFieldEffectChecker fieldEffectChecker,
            IValueInputProvider<int> valueProvider, PieceMovement start2ViaPieceMovement, PieceMovement via2EndPieceMovement, Action<PieceMoveResult> callback, Action onPiecesChanged, bool isTurnEnd)
        {
            this.player = moveActionData?.Player ?? throw new ArgumentNullException("駒を操作するプレイヤーを指定してください.");
            this.pieces = pieces ?? throw new ArgumentNullException("盤面の情報を入力してください.");
            //fieldEffectChecker ?? throw new ArgumentNullException("フィールド効果の情報を入力してください.");
            //valueProvider ?? throw new ArgumentNullException("投げ棒の値を提供するインスタンスを指定してください.");

            startPosition = moveActionData.MovingPiece.Position;    //worldPathに開始地点は含まれないのでこの方法で開始地点を取得
            viaPosition = moveActionData.ViaPositionNode.Value.Positin;
            endPosition = moveActionData.WorldPath.Last.Value.Positin;
            
            this.worldPath  = moveActionData.WorldPath;
            this.viaPieceMovement = start2ViaPieceMovement;
            this.callback = callback;
            this.isTurnEnd = isTurnEnd;

            pickupper = new Pickupper(pieces);
            pieceMover = new Mover(pieces, onPiecesChanged);
            waterEntryChecker = new WaterEntryChecker(3, fieldEffectChecker, valueProvider, OnFailure);
            moveFinisher = new MoveFinisher(pieceMover, new Pickupper(pieces));
            surmountingChecker = new SurmountingChecker(pieceMover);
            steppingChecker = new SteppingChecker(moveFinisher, pickupper, pieceMover, callback);
        }

        void OnFailure(IPiece movingPiece)
        {
            pieceMover.MovePiece(movingPiece, startPosition, true);
            callback(new PieceMoveResult(isSuccess: false, isTurnEnd: false, gottenPiece: null));
        }

        public void StartMove()
        {
            IPiece movingPiece = pieces.Read(startPosition);

            //入水判定の必要があるか
            if (!waterEntryChecker.CheckWaterEntry(movingPiece, startPosition, endPosition, () => Move(movingPiece, worldPath.First)))
                return;
            
            Move(movingPiece, worldPath.First);
        }

        void Move(IPiece movingPiece, LinkedListNode<ColumnData> worldPathNode)
        {
            if (worldPathNode == null)
            {
                moveFinisher.FinishMove(player, movingPiece, worldPath.Last.Value.Positin, callback, isTurnEnd);
                return;
            }

            IPiece nextPiece = worldPathNode.Value.Piece;

            //経由点にいる場合
            Action moveAfterNext = () => Move(movingPiece, worldPathNode.Next.Next);
            if (!steppingChecker.CheckStepping(viaPosition, player, movingPiece, worldPathNode, moveAfterNext, OnFailure))
                return;

            //PieceMovementが踏み越えに対応しているか
            Action surmountAction = () =>
            {
                surmounted = true;
                if (worldPathNode.Next.Next == null)
                {
                    moveFinisher.FinishMove(player, movingPiece, worldPathNode.Next.Value.Positin, callback, isTurnEnd);
                    return;
                }

                Move(movingPiece, worldPathNode.Next.Next);
            };
            if (!surmountingChecker.CheckSurmounting(viaPieceMovement, movingPiece, worldPathNode, surmounted, surmountAction))
                return;

            if (!moveFinisher.CheckIfContinuable(player, movingPiece, worldPathNode, callback, () => OnFailure(movingPiece), isTurnEnd))
                return;

            if (worldPathNode.Next == null)
                moveFinisher.FinishMove(player, movingPiece, worldPathNode.Value.Positin, callback, isTurnEnd);
            else
                Move(movingPiece, worldPathNode.Next);
        }
    }
}
