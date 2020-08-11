using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities.PublicDataType
{   
    public class VerifiedMove
    {
        public static readonly VerifiedMove InvalidMove = new VerifiedMove(null, null, null, new IntegerVector2(-1, -1));

        public IReadOnlyPiece MovingPiece { get; }
        public IReadOnlyPlayer Player { get; }
        public IEnumerable<IntegerVector2> WorldPath { get; }
        public IntegerVector2 ViaPositionNode { get; }

        internal VerifiedMove(IReadOnlyPiece movingPiece, IReadOnlyPlayer player, IEnumerable<IntegerVector2> worldPath, IntegerVector2 viaPositionNode)
        {
            MovingPiece = movingPiece;
            Player = player;
            WorldPath = worldPath;
            ViaPositionNode = viaPositionNode;
        }
    }
}