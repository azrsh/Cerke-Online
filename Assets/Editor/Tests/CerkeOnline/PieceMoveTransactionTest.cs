using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using System;
using System.Linq;
using UniRx;
using UniRx.Async;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction;

using Assert = UnityEngine.Assertions.Assert;

namespace Azarashi.CerkeOnline.Tests
{
    public class PieceMoveTransactionTest
    {
        static readonly PieceMoveResult FailureResult = new PieceMoveResult(false, false, null);

        private struct ComparablePiece : IEquatable<ComparablePiece>
        {
            public PieceName Name { get; }
            public IntegerVector2 Position { get; }
            public Encampment? Owner { get; }
            public PieceColor Color { get; }

            public ComparablePiece(IPiece piece)
            {
                Name = piece.Name;
                Position = piece.Position;
                Owner = piece.Owner?.Encampment;
                Color = piece.Color;
            }

            public bool Equals(ComparablePiece other)
            {
                if (Position != other.Position)
                {
                    //UnityEngine.Debug.Log("my " + other.Position);
                    //UnityEngine.Debug.Log("your " + Position);
                }

                return Name == other.Name &&
                Position == other.Position &&
                Owner == other.Owner &&
                Color == other.Color;
            }

            public override bool Equals(object obj) => Equals(obj as ComparablePiece? ?? new ComparablePiece());
            public override int GetHashCode() => (Name, Position, Owner, Color).GetHashCode();
        }

        [UnityTest]
        public IEnumerator PieceMoveTransactionTestRollBackPass() => UniTask.ToCoroutine(async () =>
        {
            const int Width = 9, Height = 9;
            var allColumn = Enumerable.Range(0, Width * Height).Select(i => new IntegerVector2(i % Width, i / Width));

            var tam = new Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Pieces.Minds(PieceColor.Black, new IntegerVector2(4, 4), null, null);
            var effectChecker = new FieldEffectChecker(new PositionArrayAccessor<FieldEffect>(BoardMapGenerator.GenerateFieldEffectMap()), tam);
            var pieceMap = new PositionArrayAccessor<IPiece>(BoardMapGenerator.GeneratePiece2DMap(new MockPlayer(Encampment.Front), new MockPlayer(Encampment.Back), tam, effectChecker));

            var tempTam = new Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Pieces.Minds(PieceColor.Black, new IntegerVector2(4, 4), null, null);
            var tempEffectChecker = new FieldEffectChecker(new PositionArrayAccessor<FieldEffect>(BoardMapGenerator.GenerateFieldEffectMap()), tempTam);
            var tempPieceMap = new PositionArrayAccessor<IPiece>(BoardMapGenerator.GeneratePiece2DMap(new MockPlayer(Encampment.Front), new MockPlayer(Encampment.Back), tempTam, tempEffectChecker));

            int count = 0;

            var tasks = allColumn
            .SelectMany(start => allColumn.Select(via => (start, via)))
            .SelectMany(positions => allColumn.Select(end => (positions.start, positions.via, end)))
            .Where(positions => tempPieceMap.Read(positions.via) != null || positions.via == positions.end)
            .Select(async positions =>
            {
                var actual = await Actual(tempPieceMap, tempEffectChecker, positions.start, positions.via, positions.end);

                var temp = allColumn.Select(tempPieceMap.Read).Where(piece => piece != null).Select(piece => new ComparablePiece(piece));
                var original = allColumn.Select(pieceMap.Read).Where(piece => piece != null).Select(piece => new ComparablePiece(piece));
                var result = original.Except(temp).Count() == 0 && temp.Except(original).Count() == 0;

                if (!result)
                {
                    string logText = "count : " + count.ToString() + "\ndiff1 : " + original.Except(temp).Count().ToString() + "\ndiff2 : " + temp.Except(original).Count().ToString() + "\n";
                    logText += "diff1\n";
                    foreach (var d1 in original.Except(temp))
                        logText += d1.Name.ToString() + "\n";
                    logText += "diff2\n";
                    foreach (var d2 in temp.Except(temp))
                        logText += d2.Name.ToString() + "\n";
                    UnityEngine.Debug.Log(logText);
                }
                count++;
                
                Assert.IsTrue(result);
            });

            foreach (var task in tasks)
                await task;
        });

        [UnityTest]
        public IEnumerator PieceMoveTransactionTestLogicPass() => UniTask.ToCoroutine(async () =>
        {
            const int Width = 9, Height = 9;
            var allColumn = Enumerable.Range(0, Width * Height).Select(i => new IntegerVector2(i % Width, i / Width));

            var tam = new Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Pieces.Minds(PieceColor.Black, new IntegerVector2(4, 4), null, null);
            var effectChecker = new FieldEffectChecker(new PositionArrayAccessor<FieldEffect>(BoardMapGenerator.GenerateFieldEffectMap()), tam);
            var pieceMap = new PositionArrayAccessor<IPiece>(BoardMapGenerator.GeneratePiece2DMap(new MockPlayer(Encampment.Front), new MockPlayer(Encampment.Back), tam, effectChecker));
            
            var tasks = allColumn
            .Where(start => pieceMap.Read(start) != null)
            .SelectMany(start => allColumn.Select(via => (start, via)))
            .SelectMany(positions => allColumn.Select(end => (positions.start, positions.via, end)))
            .Where(positions => pieceMap.Read(positions.via) != null || positions.via == positions.end)
            .Select(async positions =>
            {
                var actual = await Actual(pieceMap, effectChecker, positions.start, positions.via, positions.end);
                var expected = Expexted(pieceMap, positions);

                var assert = expected == actual.isSuccess;
                if (!assert)
                    Log("start : " + positions.start.ToString() + "\n" +
                        "via : " + positions.via.ToString() + "\n" +
                        "end : " + positions.end.ToString() + "\n" +
                        "expected : " + expected.ToString() + "\n" + 
                        "actual : " + actual.isSuccess.ToString());
                Assert.IsTrue(assert);
            });

            foreach (var task in tasks)
                await task;
        });

        bool Expexted(PositionArrayAccessor<IPiece> pieceMap, (IntegerVector2 start, IntegerVector2 via, IntegerVector2 end) positions)
        {
            bool isNoMove = positions.start == positions.via;
            if (isNoMove)
                return false;

            IPiece movingPiece = pieceMap.Read(positions.start);
            IPiece viaPiece = pieceMap.Read(positions.via);
            IPiece originalPiece = pieceMap.Read(positions.end);     //元からある駒の意味で使っているが, 英語があってるか不明.
            bool isTargetNull = movingPiece == null;
            if (isTargetNull)
                return false;

            bool isStepping = positions.via != positions.end;
            bool isViaPieceNull = viaPiece == null;
            if (isStepping && isViaPieceNull)
                return false;

            bool isOwner = true;
            bool isSameOwner = originalPiece != null && originalPiece.Owner == movingPiece.Owner /*&& originalPiece != movingPiece */; //開始地点と終了地点が同じ場合を考慮する(現在は同じ場合を許さない)
            bool isNeutral = originalPiece != null && originalPiece.Owner == null;  //Minds対策
            if (!isOwner || isSameOwner || isNeutral)
                return false;
            if (positions.start == new IntegerVector2(0, 0) && positions.via == new IntegerVector2(0, 1) && positions.end == new IntegerVector2(0, 2))
                UnityEngine.Debug.Log(originalPiece != null);

            Encampment movingPieceEncampment = movingPiece.Owner?.Encampment ?? Encampment.Front;   //Minds対策
            int positionSign = movingPieceEncampment == Encampment.Front ? -1 : 1;
            IntegerVector2 relativeStart2Via = (positions.via - positions.start) * positionSign;
            IntegerVector2 relativeVia2End = (positions.end - positions.via) * positionSign;
            PieceMovement start2ViaPieceMovement = PieceMovement.Default;
            PieceMovement via2EndPieceMovement = PieceMovement.Default;
            bool isMoveableToVia = movingPiece.TryToGetPieceMovementByRelativePosition(relativeStart2Via, out start2ViaPieceMovement);
            bool isMoveableToLast = (!isStepping || movingPiece.TryToGetPieceMovementByRelativePosition(relativeVia2End, out via2EndPieceMovement));
            if (!isMoveableToVia || !isMoveableToLast)
                return false;

            int numberOfPieceOnPath = 0;
            var via2EndPath = via2EndPieceMovement.GetPath(relativeVia2End);
            if (via2EndPath != null)
                numberOfPieceOnPath += via2EndPath.Select(position => position * positionSign + positions.via).Where(position => position != positions.end)
                    .Select(position => pieceMap.Read(position)).Where(piece => piece != null).Count();
            var start2ViaPath = start2ViaPieceMovement.GetPath(relativeStart2Via);
            if (start2ViaPath != null)
                numberOfPieceOnPath += start2ViaPath.Select(position => position * positionSign + positions.start).Where(position => position != positions.via)
                    .Select(position => pieceMap.Read(position)).Where(piece => piece != null).Count();

            return numberOfPieceOnPath <= (start2ViaPieceMovement.Surmountable ? 1 : 0);
        }

        async UniTask<PieceMoveResult> Actual(PositionArrayAccessor<IPiece> pieceMap, FieldEffectChecker effectChecker, IntegerVector2 start, IntegerVector2 via, IntegerVector2 end)
        {   
            bool areViaAndLastSame = via == end;
            IPiece movingPiece = pieceMap.Read(start);

            //------------------------------------------------
            /*bool areViaAndLastSame = viaPosition == endPosition;
            IPiece movingPiece = pieces.Read(startPosition);
            IPiece viaPiece = pieces.Read(viaPosition);
            IPiece originalPiece = pieces.Read(endPosition);     //元からある駒の意味で使っているが, 英語があってるか不明.
            bool isTargetNull = movingPiece == null;
            bool isViaPieceNull = viaPiece == null;//
            bool isOwner = !isTargetNull && movingPiece.IsOwner(player);
            bool isSameOwner = !isTargetNull && originalPiece != null && originalPiece.Owner == movingPiece.Owner;
            PieceMovement start2ViaPieceMovement = PieceMovement.Default;
            PieceMovement via2EndPieceMovement = PieceMovement.Default;
            bool isMoveableToVia = !isTargetNull && movingPiece.TryToGetPieceMovement(viaPosition, out start2ViaPieceMovement);//
            bool isMoveableToLast = !isTargetNull && (areViaAndLastSame || movingPiece.TryToGetPieceMovement(startPosition + endPosition - viaPosition, out via2EndPieceMovement));//
            if (isTargetNull || (!areViaAndLastSame && isViaPieceNull) || !isOwner || isSameOwner || !isMoveableToVia || !isMoveableToLast)//
            {
                return new PieceMoveResult(false, false, null);
            }*/
            //------------------------------------------------

            if (movingPiece == null)
                return FailureResult;
            
            PieceMovement start2ViaPieceMovement = PieceMovement.Default;
            PieceMovement via2EndPieceMovement = PieceMovement.Default;
            movingPiece.TryToGetPieceMovement(via, out start2ViaPieceMovement);
            if (!areViaAndLastSame)
                movingPiece.TryToGetPieceMovement(start + end - via, out via2EndPieceMovement);

            PieceMoveResult result = FailureResult;
            IPieceMoveTransaction pieceMoveAction = null;
            try
            {
                pieceMoveAction = new PieceMoveTransactionFactory()
                        .Create(movingPiece.Owner ?? new MockPlayer(Encampment.Front),  //Minds対策
                        start,
                        via,
                        end,
                        pieceMap,
                        effectChecker,
                        new ConstantProvider(5),
                        start2ViaPieceMovement,
                        via2EndPieceMovement,
                        true);
                result = await pieceMoveAction.StartMove();
            }
            catch (Exception e)
            {
                //if (logCount < logLimit)
                //    UnityEngine.Debug.LogError(e);
                //logCount++;
                
                result = FailureResult;
            }
            finally
            {
                pieceMoveAction?.RollBack();
            }
            return result;
        }

        const int logLimit = 50;
        int logCount = 0;
        void Log(string message)
        {
            if (logCount < logLimit)
            {
                UnityEngine.Debug.LogError(message);
                logCount++;
            }
        }

        private class MockPlayer : IPlayer
        {
            public Encampment Encampment { get; }

            public IObservable<Unit> OnPieceStrageCahnged => throw new NotImplementedException();

            public MockPlayer(Encampment encampment)
            {
                this.Encampment = encampment;
            }

            public IEnumerable<IReadOnlyPiece> GetPieceList()
            {
                throw new NotImplementedException();
            }

            public void GivePiece(IPiece piece)
            {
                throw new NotImplementedException();
            }

            public void UseCapturedPiece(IPiece piece)
            {
                //PieceMoveTransactionで呼ばれるため、空メソッドにしておく
                //PieceMoveTransaction内でのUsePieceの呼び出しをしていいかは検討の余地あり
                //throw new NotImplementedException();
            }
        }
    }
}
