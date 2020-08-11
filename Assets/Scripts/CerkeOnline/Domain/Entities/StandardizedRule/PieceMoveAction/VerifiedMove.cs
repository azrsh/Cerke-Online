using System.Collections.Generic;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction
{   
    internal class VerifiedMove
    {
        public static readonly VerifiedMove InvalidMove = new VerifiedMove(null, null, null, new IntegerVector2(-1, -1));

        public IReadOnlyPiece MovingPiece { get; }
        public IReadOnlyPlayer Player { get; }
        public IEnumerable<IntegerVector2> WorldPath { get; }
        public IntegerVector2 ViaPositionNode { get; }

        public VerifiedMove(IReadOnlyPiece movingPiece, IReadOnlyPlayer player, IEnumerable<IntegerVector2> worldPath, IntegerVector2 viaPositionNode)
        {
            MovingPiece = movingPiece;
            Player = player;
            WorldPath = worldPath;
            ViaPositionNode = viaPositionNode;
        }
    }
}