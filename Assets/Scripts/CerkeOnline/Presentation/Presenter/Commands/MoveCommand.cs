using System;
using UnityEngine;
using Azarashi.CerkeOnline.Domain.UseCase;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Commands
{
    public class MoveCommand : ICommand
    {
        readonly Vector2Int startPosition;
        readonly Vector2Int endPosition;

        public MoveCommand(Vector2Int startPosition, Type pieceType, Vector2Int endPosition)
        {
            //if (pieceType.GetInterface(nameof(IPiece)) == null)
            //    throw new InvalidOperationException();

            this.startPosition = startPosition;
            this.endPosition = endPosition;
        }

        public CommandResult Execute()
        {
            //MovePieceUseCaseFactory.Create(,)
            new PlayerMovePieceUseCase(null, null, null, null);
            return new CommandResult(true, "Move" + "[piece]" + " from " + startPosition + " to " + endPosition + "");
        }
    }
}