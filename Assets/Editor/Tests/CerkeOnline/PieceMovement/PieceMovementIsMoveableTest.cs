using NUnit.Framework;
using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;
using Assert = NUnit.Framework.Assert;
 
namespace Azarashi.CerkeOnline.Tests
{
    public class PieceMovementIsMoveableTest
    {
        [Test]
        public void Case1()
        {
            PieceMovement pieceMovement = new PieceMovement(new Vector2Int(0, 1), 1);

            Assert.IsTrue(pieceMovement.IsMoveable(new Vector2Int(0, 1)));

            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(1, 1)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(-1, 1)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(0, -1)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(-1, 0)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(1, 0)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(1, -1)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(-1, -1)));

            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(-2, 2)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(-1, 2)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(0, 2)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(1, 2)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(2, 2)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(-2, 1)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(2, 1)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(-2, 0)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(2, 0)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(-2, -1)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(2, -1)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(-2, -2)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(-1, -2)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(0, -2)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(1, -2)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(2, -2)));
        }

        [Test]
        public void Case2()
        {
            PieceMovement pieceMovement = new PieceMovement(new Vector2Int(0, 1), -1);
            for (int i = 1; i <= 10; i++)
                Assert.IsTrue(pieceMovement.IsMoveable(new Vector2Int(0, i)));

            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(1, 1)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(-1, 1)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(0, -1)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(-1, 0)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(1, 0)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(1, -1)));
            Assert.IsFalse(pieceMovement.IsMoveable(new Vector2Int(-1, -1)));
        }
    }
}
