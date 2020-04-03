using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using UniRx;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders;

namespace Azarashi.CerkeOnline.Tests.HandSuccessCheckerTest
{
    internal class HandSuccessCheckerTest
    {
        [Test]
        public void HandSuccessCheckerTestSimplePasses()
        {
            IPieceStacksProvider[] pieceStacksProviders = { new TheUnbeatable(), new TheComrades(), new TheArmy(), new TheCulture(),
                new TheKing(), new TheAttack(), new TheSocialOrder(), new TheDeadlyArmy(), new TheAnimals(), new TheCavalry()};

            foreach (var item in pieceStacksProviders)
            {
                HandSuccessChecker handSuccessChecker = new HandSuccessChecker(item);
                Assert.IsTrue(handSuccessChecker.Check(ConvertPieceStacksToPieces(item.GetPieceStacks())));
                Assert.IsFalse(handSuccessChecker.Check(Enumerable.Empty<IPiece>()));
            }
        }

        static IEnumerable<IReadOnlyPiece> ConvertPieceStacksToPieces(IEnumerable<PieceStack> pieceStacks)
        {
            return pieceStacks.SelectMany(stack =>
                    Enumerable.Repeat(new MockPiece(stack.PieceName), stack.StackCount));
        }

        private class MockPiece : IReadOnlyPiece
        {
            public IPlayer Owner => throw new System.NotImplementedException();
            public Terminologies.PieceName PieceName { get; }
            public IntegerVector2 Position => throw new System.NotImplementedException();
            public Terminologies.PieceColor Color => throw new System.NotImplementedException();
            public int NumberOfMoves => throw new System.NotImplementedException();

            public MockPiece(Terminologies.PieceName pieceName)
            {
                PieceName = pieceName;
            }

            public bool CanLittuaWithoutJudge() => throw new System.NotImplementedException();
            public bool CanTakePiece() => throw new System.NotImplementedException();
            public IEnumerable<PieceMovement> GetMoveablePosition(bool isExpanded = false) => throw new System.NotImplementedException();
            public bool IsCapturable() => throw new System.NotImplementedException();
            public bool IsMoveable(IntegerVector2 position) => throw new System.NotImplementedException();
            public bool IsOwner(IPlayer player) => throw new System.NotImplementedException();
            public bool TryToGetPieceMovement(IntegerVector2 worldPosition, out PieceMovement pieceMovement) => throw new System.NotImplementedException();
            public bool TryToGetPieceMovementByRelativePosition(IntegerVector2 relativePosition, out PieceMovement pieceMovement) => throw new System.NotImplementedException();
        }
    }
}
