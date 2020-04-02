using NUnit.Framework;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule;

namespace Azarashi.CerkeOnline.Tests.HandDatabaseTest
{
    internal class HandDatabaseSearchHands
    {
        [Test]
        public void HandDatabaseSearchHandsSimplePasses()
        {
            //TestMethod(new HandDatabase(BoardFactory.Create(new Player(Terminologies.Encampment.Front), new Player(Terminologies.Encampment.Back)),new UniRx.Subject<UniRx.Unit>()));
        }

        void TestMethod(IHandDatabase handDatabase)
        {
            //new SozysozbotHandVerifier(handDatabase.SearchHands).Verify();
        }
    }
}
