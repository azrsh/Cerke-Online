using System;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Commands
{
    public class CommandFactory
    {
        CommandAnalyzer analyzer = new CommandAnalyzer();

        public ICommand CreateInstance(string value)
        {
            string[] words = value.Split(' ');
            CommandEnum commandEnum = CommandEnum.none;
            Enum.TryParse(words[0], true, out commandEnum);
            
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

            IntegerVector2 startPosition = analyzer.GetPosition(words[1]);
            PieceName pieceName = PieceName.None;
            Enum.TryParse(words[2], true, out pieceName);
            IntegerVector2 endPosition = analyzer.GetPosition(words[3]);
            return new MoveCommand(startPosition, pieceName, endPosition);
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