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

        public int GetNumberOfSuccesses(IReadOnlyList<IReadOnlyPiece> holdingPieces)
        {
            IReadOnlyList<PieceStack> pieceStacks = pieceStacksProvider.GetPieceStacks();

            if (holdingPieces.Count < pieceStacks.Count) return 0;

            IEnumerable<PieceName> holdingPieceNames = holdingPieces.Select(piece => piece.PieceName);
            int restAlesCount = holdingPieceNames.Where(name => name == PieceName.Ales).Count();
            bool isSuccess = pieceStacks.All(stack =>
            {
                int appropriateHoldingPieceCount = holdingPieceNames.Count(pieceName => stack.PieceName == PieceName.None || pieceName == stack.PieceName);
                int difference = appropriateHoldingPieceCount - stack.StackCount;
                bool isIndividualSuccess = difference + restAlesCount >= 0;
                restAlesCount += System.Math.Min(0, difference);
                return isIndividualSuccess;
            });
            return isSuccess ? 1 : 0;
        }
    }
}