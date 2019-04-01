using System.Linq;
using System.Collections.Generic;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    /// <summary>
    /// 同色役
    /// </summary>
    public class LaDejixeceHand : IHand
    {
        readonly IPieceStacksProvider pieceStacksProvider;

        public string Name { get; }
        public int Score { get; }

        public LaDejixeceHand(IPieceStacksProvider pieceStacksProvider, int score)
        {
            this.pieceStacksProvider = pieceStacksProvider;
            Name = "同色役";  //TODO べた書きの排除
            Score = score;
        }

        public int GetNumberOfSuccesses(IReadOnlyList<IReadOnlyPiece> pieces)
        {
            IReadOnlyList<PieceStack> pieceStacks = pieceStacksProvider.GetPieceStacks();

            if (pieces.Count < pieceStacks.Count) return 0;

            bool black = pieceStacks.All(stack =>
            {
                return pieces.Count(piece => piece.Color == 0 && (stack.PieceName == PieceName.None || piece.PieceName == stack.PieceName)) >= stack.StackCount;
            });
            bool red = pieceStacks.All(stack =>
            {
                return pieces.Count(piece => piece.Color == 1 && (stack.PieceName == PieceName.None || piece.PieceName == stack.PieceName)) >= stack.StackCount;
            });

            bool isSuccess = black || red;
            return isSuccess ? 1 : 0;
        }
    }
}