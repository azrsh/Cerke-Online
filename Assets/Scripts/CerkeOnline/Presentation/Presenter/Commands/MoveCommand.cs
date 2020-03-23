using System;
using UnityEngine;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;
using Azarashi.CerkeOnline.Application;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Commands
{
    public class MoveCommand : ICommand
    {
        readonly Vector2Int startPosition;
        readonly PieceName pieceName;
        readonly Vector2Int endPosition;

        public MoveCommand(Vector2Int startPosition, PieceName pieceName, Vector2Int endPosition)
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