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
            Name = "同色" + HandNameDictionary.PascalToJapanese[pieceStacksProvider.GetType().Name]; //TODO べた書きの排除
            Score = score;
        }

        public int GetNumberOfSuccesses(IReadOnlyList<IReadOnlyPiece> holdingPieces)
        {
            IReadOnlyList<PieceStack> pieceStacks = pieceStacksProvider.GetPieceStacks();

            if (holdingPieces.Count < pieceStacks.Count) return 0;

            IEnumerable<IReadOnlyPiece> alesList = holdingPieces.Where(piece => piece.PieceName == PieceName.Ales);
            int restBlackAlesCount = alesList.Where(piece => piece.Color == PieceColor.Black).Count();
            bool black = pieceStacks.All(stack =>
            {
                int appropriateHoldingPieceCount = holdingPieces.Count(piece => piece.Color == PieceColor.Black && (stack.PieceName == PieceName.None || piece.PieceName == stack.PieceName));
                int difference = appropriateHoldingPieceCount - stack.StackCount;
                bool isIndividualSuccess = difference + restBlackAlesCount >= 0;
                restBlackAlesCount += System.Math.Min(0, difference);
                return isIndividualSuccess;
            });
            
            int restRedAlesCount = alesList.Where(piece => piece.Color == PieceColor.Red).Count();
            bool red = pieceStacks.All(stack =>
            {
                int appropriateHoldingPieceCount = holdingPieces.Count(piece => piece.Color == PieceColor.Red && (stack.PieceName == PieceName.None || piece.PieceName == stack.PieceName));
                int difference = appropriateHoldingPieceCount - stack.StackCount;
                bool isIndividualSuccess = difference + restRedAlesCount >= 0;
                restRedAlesCount += System.Math.Min(0, difference);
                return isIndividualSuccess;
            }); 

            bool isSuccess = black || red;
            return isSuccess ? 1 : 0;
        }
    }
}