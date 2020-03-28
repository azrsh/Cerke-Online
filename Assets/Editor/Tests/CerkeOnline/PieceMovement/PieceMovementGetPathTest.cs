using NUnit.Framework;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using Assert = NUnit.Framework.Assert;
 
namespace Azarashi.CerkeOnline.Tests.PieceMovementTest
{
    internal class PieceMovementGetPathTest
    {
        [Test]
        public void Case1()
        {
            PieceMovement pieceMovement = new PieceMovement(new IntVector2(0, 1), 1);

            Assert.IsNotNull(pieceMovement.GetPath(new IntVector2(0, 1)));

            Assert.IsNull(pieceMovement.GetPath(new IntVector2(1, 1)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(-1, 1)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(0, -1)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(-1, 0)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(1, 0)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(1, -1)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(-1, -1)));

            Assert.IsNull(pieceMovement.GetPath(new IntVector2(-2, 2)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(-1, 2)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(0, 2)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(1, 2)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(2, 2)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(-2, 1)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(2, 1)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(-2, 0)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(2, 0)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(-2, -1)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(2, -1)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(-2, -2)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(-1, -2)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(0, -2)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(1, -2)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(2, -2)));
        }

        
        [Test]
        public void Case2()
        {
            PieceMovement pieceMovement = new PieceMovement(new IntVector2(0, 1), -1);
            for (int i = 1; i <= 10; i++)
                Assert.IsNotNull(pieceMovement.GetPath(new IntVector2(0, i)));

            Assert.IsNull(pieceMovement.GetPath(new IntVector2(1, 1)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(-1, 1)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(0, -1)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(-1, 0)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(1, 0)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(1, -1)));
            Assert.IsNull(pieceMovement.GetPath(new IntVector2(-1, -1)));
        }
    }
}
