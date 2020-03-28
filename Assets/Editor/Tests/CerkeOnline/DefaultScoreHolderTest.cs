using System.Collections.Generic;
using NUnit.Framework;
using Azarashi.CerkeOnline.Domain.Entities;
using System;
using UniRx;

namespace Azarashi.CerkeOnline.Tests.DefaultScoreHolderTest
{ 
    internal class DefaultScoreHolderTest
    {
        [Test]
        public void DefaultScoreHolderTestSimplePasses()
        {
            IPlayer player1 = new MockPlayer();
            IPlayer player2 = new MockPlayer();
            IScoreHolder scoreHolder = new DefaultScoreHolder(new Dictionary<IPlayer, int> { { player1, 30 },{ player2, 30 } });

            scoreHolder.MoveScore(player1, 10);
            Assert.AreEqual(scoreHolder.GetScore(player1).Value, 40);
            Assert.AreEqual(scoreHolder.GetScore(player2).Value, 20);

            scoreHolder.MoveScore(player1, 50);
            Assert.AreEqual(scoreHolder.GetScore(player1).Value, 60);
            Assert.AreEqual(scoreHolder.GetScore(player2).Value, 0);

            IntReactiveProperty property = null;
            scoreHolder.TryGetScore(new MockPlayer(), out property);
            Assert.IsNull(property);

            Assert.Catch(typeof(KeyNotFoundException), () => scoreHolder.GetScore(new MockPlayer()));
        }

        private class MockPlayer : IPlayer
        {
            public Terminologies.Encampment Encampment => throw new NotImplementedException();

            public IObservable<Unit> OnPieceStrageCahnged => throw new NotImplementedException();

            public IReadOnlyList<IReadOnlyPiece> GetPieceList()
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
