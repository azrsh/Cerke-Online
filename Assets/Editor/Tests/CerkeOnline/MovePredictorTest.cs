using System.Collections.Generic;
using NUnit.Framework;
using System;
using UniRx;
using Azarashi.Utilities.Collections;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Pieces;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Tests
{
    public class MovePredictorTest
    {
        readonly IBoard board = new MockBoard();

        [Test]
        public void MovePredictorTestSimplePasses()
        {
            for (int i = 0; i < 100; i++)
                RandomCase();
        }

        void RandomCase()
        {
            var random = new Random(DateTime.Now.Millisecond);
            var randomPosition1 = GetRandam(random);
            var randomPosition2 = GetRandam(random);
            var result = new MovePredictor(board)
                        .PredictMoveableColumns
                        (   
                            randomPosition1, 
                            new King(
                                PieceColor.Black,randomPosition2, new MockPlayer(Encampment.Front), new MockFieldEffectChecker()
                            )
                        );

            var example = PredictMoveableColumns
                (
                    randomPosition1,
                    new King
                    (
                        PieceColor.Black, randomPosition2, new MockPlayer(Encampment.Front), new MockFieldEffectChecker()
                    )
                );

            Assert.IsTrue(result.SequenceMatch(example));
        }

        IntegerVector2 GetRandam(Random random)
        {
            return new IntegerVector2(random.Next() % board.Width, random.Next() % board.Height);
        }

        IEnumerable<IntegerVector2> PredictMoveableColumns(IntegerVector2 hypotheticalPosition, IReadOnlyPiece movingPiece)
        {
            List<IntegerVector2> result = new List<IntegerVector2>();

            if (movingPiece == null || !board.IsOnBoard(hypotheticalPosition)) return result;

            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    IntegerVector2 currentPosition = new IntegerVector2(i, j);
                    bool isFrontPlayer = movingPiece.Owner != null && movingPiece.Owner.Encampment == Encampment.Front;
                    IntegerVector2 relativePosition = (currentPosition - hypotheticalPosition) * (isFrontPlayer ? -1 : 1);
                    PieceMovement unused;
                    if (board.IsOnBoard(currentPosition) &&
                        movingPiece.TryToGetPieceMovementByRelativePosition(relativePosition, out unused))
                        result.Add(currentPosition);
                }
            }

            return result;
        }


        private class MockBoard : IBoard
        {
            public IObservable<Unit> OnEveruValueChanged => throw new NotImplementedException();

            public int Width { get; } = 9;

            public int Height { get; } = 9;

            public IReadOnlyPiece GetPiece(IntegerVector2 position)
            {
                throw new NotImplementedException();
            }

            public bool IsOnBoard(IntegerVector2 position) => position.x >= 0 && position.y >= 0 && position.x < Width && position.y < Height;

            public void MovePiece(IntegerVector2 startPosition, IntegerVector2 endPosition, IPlayer player, IValueInputProvider<int> valueProvider, Action<PieceMoveResult> callback)
            {
                throw new NotImplementedException();
            }

            public void MovePiece(IntegerVector2 startPosition, IntegerVector2 viaPosition, IntegerVector2 endPosition, IPlayer player, IValueInputProvider<int> valueProvider, Action<PieceMoveResult> callback)
            {
                throw new NotImplementedException();
            }

            public IReadOnlyPiece SearchPiece(Terminologies.PieceName pieceName)
            {
                throw new NotImplementedException();
            }

            public bool SetPiece(IntegerVector2 position, IPiece piece)
            {
                throw new NotImplementedException();
            }
        }

        private class MockFieldEffectChecker : IExpandingMoveFieldChecker
        {
            public bool IsExpandedMoveField(IntegerVector2 position) => false;
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
                throw new NotImplementedException();
            }
        }
    }
}
