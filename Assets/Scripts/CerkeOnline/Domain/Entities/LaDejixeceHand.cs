using System.Linq;
using System.Collections.Generic;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public class LaDejixeceHand : IHand
    {
        readonly IPieceStacksProvider pieceStacksProvider;

        public string Name { get { return pieceStacksProvider.GetType().Name; } }
        public int Score { get; private set; }

        protected LaDejixeceHand(IPieceStacksProvider pieceStacksProvider, int score)
        {
            this.pieceStacksProvider = pieceStacksProvider;
            Score = score;
        }

        public bool IsSccess(IReadOnlyList<IReadOnlyPiece> pieces)
        {
            IReadOnlyList<PieceStack> pieceStacks = pieceStacksProvider.GetPieceStacks();

            if (pieces.Count < pieceStacks.Count) return false;

            IEnumerable<PieceName> pieceNames = pieces.Select(piece => piece.PieceName);
            //UnityEngine.Color color = default;
            return pieceStacks.All(stack =>
            {
                //if(color == default) 
                return pieceNames.Count(pieceName => stack.PieceName == PieceName.None || pieceName == stack.PieceName) >= stack.StackCount;
            });
        }
    }
}