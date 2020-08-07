using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Async;
using Azarashi.Utilities.Assertions;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
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

    internal class PieceMoveTransaction : IPieceMoveTransaction
    {
        readonly IPlayer player;
        readonly PositionArrayAccessor<IPiece> pieces;
        readonly LinkedList<ColumnData> worldPath;
        readonly IValueInputProvider<int> valueProvider;
        readonly bool surmountableOnVia2End;
        readonly bool isTurnEnd;

        readonly PublicDataType.IntegerVector2 startPosition;
        readonly PublicDataType.IntegerVector2 viaPosition;
        readonly PublicDataType.IntegerVector2 endPosition;

        readonly Mover pieceMover;
        readonly WaterEntryChecker waterEntryChecker;
        readonly MoveFinisher moveFinisher;

        IPiece movingPiece; //readonlyでキャッシュすべきかも
        Option<CaptureResult> captureResult = new Option<CaptureResult>();

        public PieceMoveTransaction(MoveActionData moveActionData, PositionArrayAccessor<IPiece> pieces, IFieldEffectChecker fieldEffectChecker,
            IValueInputProvider<int> valueProvider, bool surmountableOnVia2End, bool isTurnEnd)
        {
            Assert.IsNotNull(moveActionData);
            Assert.IsNotNull(moveActionData.Player);
            Assert.IsNotNull(pieces);
            Assert.IsNotNull(fieldEffectChecker);
            Assert.IsNotNull(valueProvider);

            this.player = moveActionData.Player;
            this.pieces = pieces;
            this.valueProvider = valueProvider;

            startPosition = moveActionData.MovingPiece.Position;    //worldPathに開始地点は含まれないのでこの方法で開始地点を取得
            viaPosition = moveActionData.ViaPositionNode.Value.Positin;
            endPosition = moveActionData.WorldPath.Last.Value.Positin;
            
            this.worldPath  = moveActionData.WorldPath;
            this.surmountableOnVia2End = surmountableOnVia2End;
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
            PieceMoveResult failureReasult = new PieceMoveResult(false, false, null);

            if (!await waterEntryChecker.CheckWaterEntry(movingPiece, startPosition, endPosition))
                return failureReasult;

            var surmountLimit = surmountableOnVia2End ? 1 : 0;
            var noPieceOnPath = worldPath.Where(node => node.Positin != viaPosition).Where(node => node.Positin != endPosition)
                .Select(node => node.Piece).Where(piece => piece != null)
                .Count() <= surmountLimit;
            if (!noPieceOnPath) return failureReasult;
            
            if (viaPosition != endPosition)
            {
                var steppedList = worldPath.SkipWhile(node => node.Positin != viaPosition);

                var steppingNode = steppedList.First();

                var nextNode = steppedList.Skip(1).First();
                if (steppingNode.Piece is ISteppedObserver)
                    (steppingNode.Piece as ISteppedObserver).OnSteppied.OnNext(Unit.Default);

                int afterStepLimit = await valueProvider.RequestValue();
                if (steppedList.Skip(1).Count() > afterStepLimit)
                    return failureReasult;  //ここまでは駒を動かしていないので判定はいらない
            }

            var result = moveFinisher.ConfirmMove(player, movingPiece, endPosition, isTurnEnd, true);
            captureResult = new Option<CaptureResult>(result.captureResult);
            
            //ここで行うのではなく利用者側が責任を持つべきな気もする
            if (!result.pieceMoveResult.isSuccess)
                RollBack();
            
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
