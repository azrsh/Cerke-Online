using System;
using System.Linq;
using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Commands
{
    public class CommandFactory
    {
        CommandAnalyzer analyzer = new CommandAnalyzer();

        public ICommand CreateInstance(string value)
        {
            string[] words = value.Split(' ');
            CommandEnum commandEnum = CommandEnum.none;
            Enum.TryParse(words[0], out commandEnum);
            
            switch(commandEnum)
            {
            case CommandEnum.move:
                return CreateMoveCommandInstance(words);
            case CommandEnum.set:
                return CreateSetCommandInstance(words);
            case CommandEnum.turnend:
                return CreateTurnEndCommandInstance(words);
            case CommandEnum.extend:
                return CreateExtendCommandInstance(words);
            case CommandEnum.undo:
                return CreateUndoCommandInstance(words);
            case CommandEnum.quit:
                return CreateQuitCommandInstance(words);
            default:
                return null;
            }
        }

        public ICommand CreateMoveCommandInstance(string[] words)
        {
            if (!analyzer.CheckFormat(words, 4)/* || words[0] != CommandEnum.move.ToString()*/)
                return null;

            Vector2Int startPosition = analyzer.GetPosition(words[1]);
            string pieceName = words[2];
            Vector2Int endPosition = analyzer.GetPosition(words[3]);
            return new MoveCommand(startPosition, typeof(IPiece), endPosition);
        }

        public ICommand CreateSetCommandInstance(string[] words)
        {
            return null;
        }

        public ICommand CreateTurnEndCommandInstance(string[] words)
        {
            return null;
        }

        public ICommand CreateExtendCommandInstance(string[] words)
        {
            return null;
        }

        public ICommand CreateUndoCommandInstance(string[] words)
        {
            return null;
        }

        public ICommand CreateQuitCommandInstance(string[] words)
        {
            return null;
        }
    }
}