using NUnit.Framework;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.Official;

namespace Azarashi.CerkeOnline.Tests
{
    public class HandDatabaseSearchHands
    {
        [Test]
        public void HandDatabaseSearchHandsSimplePasses()
        {
            TestMethod(new HandDatabase(BoardFactory.Create(new Player(Terminologies.Encampment.Front), new Player(Terminologies.Encampment.Back)),new UniRx.Subject<UniRx.Unit>()));
        }

        void TestMethod(IHandDatabase handDatabase)
        {
            new SozysozbotHandVerificater(handDatabase.SearchHands).Verify();
        }
    }
}
