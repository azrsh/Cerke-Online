using System.Linq;
using System.Collections.Generic;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public class DefaultHand : IHand
    {   
        readonly IPieceStacksProvider pieceStacksProvider;

        public string Name { get { return pieceStacksProvider.GetType().Name; } }
        public int Score { get; private set; }

        protected DefaultHand(IPieceStacksProvider pieceStacksProvider, int score)
        {
            this.pieceStacksProvider = pieceStacksProvider;
            Score = score;
        }

        public bool IsSccess(IReadOnlyList<IReadOnlyPiece> pieces)
        {
            IReadOnlyList<PieceStack> pieceStacks = pieceStacksProvider.GetPieceStacks();

            if (pieces.Count < pieceStacks.Count) return false;

            IEnumerable<PieceName> pieceNames = pieces.Select(piece => piece.PieceName);
            return pieceStacks.All(stack => pieceNames.Count(pieceName => stack.PieceName == PieceName.None || pieceName == stack.PieceName) >= stack.StackCount);
        }
    }
}