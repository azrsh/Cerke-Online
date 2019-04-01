using System.Linq;
using System.Collections.Generic;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    /// <summary>
    /// 通常役
    /// </summary>
    public class DefaultHand : IHand
    {
        readonly IPieceStacksProvider pieceStacksProvider;

        public string Name { get; }
        public int Score { get; }

        public DefaultHand(IPieceStacksProvider pieceStacksProvider, int score)
        {
            this.pieceStacksProvider = pieceStacksProvider;
            Name = HandNameDictionary.PascalToJapanese[pieceStacksProvider.GetType().Name];
            Score = score;
        }

        public int GetNumberOfSuccesses(IReadOnlyList<IReadOnlyPiece> pieces)
        {
            IReadOnlyList<PieceStack> pieceStacks = pieceStacksProvider.GetPieceStacks();

            if (pieces.Count < pieceStacks.Count) return 0;

            IEnumerable<PieceName> pieceNames = pieces.Select(piece => piece.PieceName);
            bool isSuccess = pieceStacks.All(stack => pieceNames.Count(pieceName => stack.PieceName == PieceName.None || pieceName == stack.PieceName) >= stack.StackCount);
            return isSuccess ? 1 : 0;
        }
    }
}