using System.Linq;
using System.Collections.Generic;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    internal class HandSuccessChecker
    {
        readonly IEnumerable<PieceStack> pieceStacks;

        internal HandSuccessChecker(IPieceStacksProvider pieceStacksProvider)
        {
            pieceStacks = pieceStacksProvider.GetPieceStacks();
        }

        internal bool Check(IEnumerable<IReadOnlyPiece> holdingPieces)
        {
            if (holdingPieces.Count() < pieceStacks.Count()) return false;
            
            IEnumerable<PieceName> holdingPieceNames = holdingPieces.Select(piece => piece.Name);
            int restAlesCount = holdingPieceNames.Where(name => name == PieceName.King).Count();
            bool isSuccess = pieceStacks.All(stack =>
            {
                int appropriateHoldingPieceCount = holdingPieceNames.Count(pieceName => stack.PieceName == PieceName.None || pieceName == stack.PieceName);
                int difference = appropriateHoldingPieceCount - stack.StackCount;
                bool isIndividualSuccess = difference + restAlesCount >= 0;
                restAlesCount += System.Math.Min(0, difference);
                return isIndividualSuccess;
            });
            return isSuccess;
        }
    }
}
