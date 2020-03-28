using System;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using Azarashi.CerkeOnline.Application;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Commands
{
    public class MoveCommand : ICommand
    {
        readonly IntegerVector2 startPosition;
        readonly PieceName pieceName;
        readonly IntegerVector2 endPosition;

        public MoveCommand(IntegerVector2 startPosition, PieceName pieceName, IntegerVector2 endPosition)
        {
            if (pieceName == PieceName.None)
                throw new InvalidOperationException();

            this.startPosition = startPosition;
            this.pieceName = pieceName;
            this.endPosition = endPosition;
        }

        public CommandResult Execute()
        {
            MovePieceUseCaseFactory.Create(FirstOrSecond.First, null).RequestToMovePiece(startPosition, endPosition);
            return new CommandResult(true, "Move " + pieceName.ToString() + " from " + startPosition + " to " + endPosition + ".");
        }
    }
}