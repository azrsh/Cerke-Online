﻿using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Async;
using Azarashi.Utilities.Assertions;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
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

    internal class PieceMoveTransaction : IPieceMoveTransaction
    {
        static readonly PieceMoveResult JudgeFailureReasult = new PieceMoveResult(false, true, null);

        readonly IPlayer player;
        readonly PositionArrayAccessor<IPiece> pieces;
        readonly IEnumerable<IntegerVector2> worldPath;
        readonly IValueInputProvider<int> valueProvider;
        readonly bool isTurnEnd;

        readonly IntegerVector2 startPosition;
        readonly IntegerVector2 viaPosition;
        readonly IntegerVector2 endPosition;

        readonly Mover pieceMover;
        readonly WaterEntryChecker waterEntryChecker;
        readonly MoveFinisher moveFinisher;

        IPiece movingPiece; //readonlyでキャッシュすべきかも
        Option<CaptureResult> captureResult = new Option<CaptureResult>();

        public PieceMoveTransaction(IPlayer player, VerifiedMove verifiedMove, PositionArrayAccessor<IPiece> pieces, IFieldEffectChecker fieldEffectChecker,
            IValueInputProvider<int> valueProvider, bool isTurnEnd)
        {
            Assert.IsNotNull(verifiedMove);
            Assert.IsNotNull(verifiedMove.Player);
            Assert.IsNotNull(player);
            Assert.AreEqual(verifiedMove.Player, player);
            Assert.IsNotNull(pieces);
            Assert.IsNotNull(fieldEffectChecker);
            Assert.IsNotNull(valueProvider);

            this.player = player;
            this.pieces = pieces;
            this.valueProvider = valueProvider;

            startPosition = verifiedMove.MovingPiece.Position;    //worldPathに開始地点は含まれないのでこの方法で開始地点を取得
            viaPosition = verifiedMove.ViaPositionNode;
            endPosition = verifiedMove.WorldPath.Last();
            
            this.worldPath  = verifiedMove.WorldPath;
            this.isTurnEnd = isTurnEnd;

            pieceMover = new Mover(pieces);
            waterEntryChecker = new WaterEntryChecker(3, fieldEffectChecker, valueProvider);
            moveFinisher = new MoveFinisher(pieceMover, new Capturer(pieces));
        }

        public void RollBack()
        {
            pieceMover.MovePiece(movingPiece, startPosition, true);

            if (captureResult.IsNone) return;

            var result = captureResult.GetOrDefault(new CaptureResult());
            if (result.Captured == null) return;

            player.UseCapturedPiece(result.Captured); //ここで行うべきではないかも（駒の取得は外で行っているため）
            result.Captured.SetOwner(result.FormerOwner);
            result.Captured.SetOnBoard(result.From);
            pieces.Write(result.From, result.Captured);

            captureResult = new Option<CaptureResult>();
        }

        public async UniTask<PieceMoveResult> StartMove()
        {
            movingPiece = pieces.Read(startPosition);

            //---投げ棒による判定-------
            if (!await waterEntryChecker.CheckWaterEntry(movingPiece, startPosition, endPosition))
                return JudgeFailureReasult;

            if (viaPosition != endPosition)
            {
                var steppedList = worldPath.SkipWhile(node => node != viaPosition);

                var steppingNode = steppedList.First();
                var steppingPiece = pieces.Read(steppingNode);

                var nextNode = steppedList.Skip(1).First();
                if (steppingPiece is ISteppedObserver)
                    (steppingPiece as ISteppedObserver).OnSteppied.OnNext(Unit.Default);

                int afterStepLimit = await valueProvider.RequestValue();
                if (steppedList.Skip(1).Count() > afterStepLimit)
                {
                    //ここまでは駒を動かしていないので判定はいらない
                    //判定失敗でターン終了
                    return JudgeFailureReasult;
                }
            }
            //------------------------

            //---駒の実際の移動--------
            var result = moveFinisher.ConfirmMove(player, movingPiece, endPosition, isTurnEnd, true);
            captureResult = new Option<CaptureResult>(result.captureResult);
            //------------------------

            //---失敗時のロールバック---
            //ここで行うのではなく利用者側が責任を持つべきな気もする
            if (!result.pieceMoveResult.isSuccess)
                RollBack();
            //------------------------

            return result.pieceMoveResult;
        }

        //-------------------------------------------------------------------------------
        private struct Option<T>
        {
            public bool IsNone { get; }
            readonly T value;

            public Option(T value)
            {
                IsNone = value == null;
                this.value = value; ;
            }

            public T GetOrDefault(T def)
            {
                if (IsNone)
                    return def;

                return value;
            }
        }
        //-------------------------------------------------------------------------------
    }
}
