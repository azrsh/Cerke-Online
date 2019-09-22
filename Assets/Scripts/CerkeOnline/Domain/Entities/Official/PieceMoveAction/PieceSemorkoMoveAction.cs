using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Azarashi.Utilities.Collections;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;
using Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.DataStructure;
using Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.ActualAction;
using Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.AbstractAction;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction
{
    /*命名に関する注意
     以下のクラスでは, 
     ・Surmountとその派生を巫の駒越え
     ・Semorkoを踏み越え
     という意味で使っています.
     */

    public class PieceSemorkoMoveAction
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

        readonly Mover pieceMover;
        readonly Pickupper pickupper;
        readonly WaterEntryChecker waterEntryChecker;
        readonly MoveFinisher moveFinisher;
        readonly SurmountingChecker surmountingChecker;
        readonly SemorkoChecker semorkoChecker;

        public PieceSemorkoMoveAction(IPlayer player, Vector2Int startPosition, Vector2Int viaPosition, Vector2Int lastPosition, Vector2YXArrayAccessor<IPiece> pieces, IFieldEffectChecker fieldEffectChecker,
            IValueInputProvider<int> valueProvider, PieceMovement viaPieceMovement, PieceMovement lastPieceMovement, Action<PieceMoveResult> callback, Action onPiecesChanged, bool isTurnEnd)
        {
            this.player = player ?? throw new ArgumentNullException("駒を操作するプレイヤーを指定してください.");
            this.pieces = pieces ?? throw new ArgumentNullException("盤面の情報を入力してください.");
            //fieldEffectChecker ?? throw new ArgumentNullException("フィールド効果の情報を入力してください.");
            //valueProvider ?? throw new ArgumentNullException("投げ棒の値を提供するインスタンスを指定してください.");

            this.startPosition = startPosition;
            this.viaPosition = viaPosition;
            bool isFrontPlayersPiece = pieces.Read(startPosition).Owner != null && pieces.Read(startPosition).Owner.Encampment == Encampment.Front;
            Vector2Int relativeViaPosition = (viaPosition - startPosition) * (isFrontPlayersPiece ? -1 : 1);
            var relativeViaPath = viaPieceMovement.GetPath(relativeViaPosition)?.ToList() ?? throw new ArgumentException("移動不可能な移動先が指定されました.");
            Vector2Int relativeLastPosition = (lastPosition - viaPosition) * (isFrontPlayersPiece ? -1 : 1);
            var realtiveLastPath = lastPieceMovement.GetPath(relativeLastPosition) ?? throw new ArgumentException("移動不可能な移動先が指定されました.");

            var worldPath = relativeViaPath.Select(value => startPosition + value * (isFrontPlayersPiece ? -1 : 1)).ToList();
            worldPath.AddRange(realtiveLastPath.Select(value => viaPosition + value * (isFrontPlayersPiece ? -1 : 1)));
            this.worldPath = new LinkedList<ColumnData>(worldPath.Select(value => new ColumnData(value, pieces)));

            this.viaPieceMovement = viaPieceMovement;
            this.callback = callback;
            this.isTurnEnd = isTurnEnd;

            pickupper = new Pickupper(pieces);
            pieceMover = new Mover(pieces, onPiecesChanged);
            waterEntryChecker = new WaterEntryChecker(3, fieldEffectChecker, valueProvider, OnJudgementFailure);
            moveFinisher = new MoveFinisher(pieceMover, new Pickupper(pieces));
            surmountingChecker = new SurmountingChecker(pieceMover, waterEntryChecker);
            semorkoChecker = new SemorkoChecker(waterEntryChecker, moveFinisher, pickupper, pieceMover, callback);
        }

        void OnFailure(IPiece movingPiece)
        {
            pieceMover.MovePiece(movingPiece, startPosition, true);
            callback(new PieceMoveResult(isSuccess: false, isTurnEnd: false, gottenPiece: null));
        }

        void OnJudgementFailure(IPiece movingPiece, LinkedListNode<ColumnData> worldPathNode)
        {
            if (worldPathNode?.Previous != null)
                moveFinisher.FinishMove(player, movingPiece, worldPathNode.Previous.Value.Positin, callback, isTurnEnd);
            else
                callback(new PieceMoveResult(true, isTurnEnd, null));
        }

        public void StartMove()
        {
            IPiece movingPiece = pieces.Read(startPosition);
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

            //入水判定の必要があるか
            if (!waterEntryChecker.CheckWaterEntry(movingPiece, worldPathNode, () => Move(movingPiece, worldPathNode.Next)))
                return;

            //経由点にいる場合
            IPiece semorkoNextPiece = worldPathNode.Next.Value.Piece;
            Action moveAfterNext = () => Move(movingPiece, worldPathNode.Next.Next);
            if (!semorkoChecker.CheckSemorko(viaPosition, player, movingPiece, worldPathNode, moveAfterNext, OnFailure))
                return;

            //PieceMovementが踏み越えに対応しているか
            Action surmountAction = () =>
            {
                surmounted = true;
                if (worldPathNode.Next.Value.Piece == null)
                {
                    pieceMover.MovePiece(movingPiece, worldPathNode.Next.Value.Positin, isForceMove: true);
                    Move(movingPiece, worldPathNode.Next.Next);
                    return;
                }

                if (worldPathNode.Next.Next == null)
                {
                    moveFinisher.FinishMove(player, movingPiece, worldPathNode.Next.Value.Positin, callback, isTurnEnd);
                    return;
                }

                OnFailure(movingPiece);
            };
            if (!surmountingChecker.CheckSurmounting(viaPieceMovement, movingPiece, worldPathNode, surmounted, surmountAction))
                return;

            if(!moveFinisher.CheckIfContinuable(player, movingPiece, worldPathNode, callback, () => OnFailure(movingPiece), isTurnEnd))
                return;

            if(worldPathNode.Next == null)
                moveFinisher.FinishMove(player, movingPiece, worldPathNode.Previous.Value.Positin, callback, isTurnEnd);
            else
                Move(movingPiece, worldPathNode.Next);
        }
    }
}
