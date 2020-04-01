using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public class NotYourTurnMessage : ISystemMessage { }
    public class NotYourPieceMessage : ISystemMessage { }
    public class PieceMovementMessage : ISystemMessage 
    {
        public PieceMovementMessage(IReadOnlyPlayer player, IReadOnlyPiece piece, IntegerVector2 startPosition, IntegerVector2 endPosition)
        {
            Player = player;
            Piece = piece;
            StartPosition = startPosition;
            EndPosition = endPosition;
        }

        public IReadOnlyPlayer Player { get; }
        public IReadOnlyPiece Piece { get; }
        public IntegerVector2 StartPosition { get; }
        public IntegerVector2 EndPosition { get; }
    }

    public class PieceViaMovementMessage : ISystemMessage
    {
        public PieceViaMovementMessage(IReadOnlyPlayer player, IReadOnlyPiece piece, IntegerVector2 startPosition, IntegerVector2 viaPosition, IntegerVector2 endPosition)
        {
            Player = player;
            Piece = piece;
            StartPosition = startPosition;
            ViaPosition = viaPosition;
            EndPosition = endPosition;
        }

        public IReadOnlyPlayer Player { get; }
        public IReadOnlyPiece Piece { get; }
        public IntegerVector2 StartPosition { get; }
        public IntegerVector2 ViaPosition { get; }
        public IntegerVector2 EndPosition { get; }
    }

    public class PieceMovementFailureMessage : ISystemMessage { }
    public class NoPieceViaPointMessage : ISystemMessage { }
    public class NoPieceSelectedMessage : ISystemMessage { }
    public class SetPieceMessage : ISystemMessage
    {
        public SetPieceMessage(IReadOnlyPlayer player, IReadOnlyPiece piece, IntegerVector2 setPosition)
        {
            Player = player;
            Piece = piece;
            SetPosition = setPosition;
        }

        public IReadOnlyPlayer Player { get; }
        public IReadOnlyPiece Piece { get; }
        public IntegerVector2 SetPosition { get; }
    }
    public class SetPieceFailureMessage : ISystemMessage { }
    

    public interface ISystemMessage { }
}