using System.Linq;
using System.Collections.Generic;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    /// <summary>
    /// 同色役
    /// </summary>
    internal class FlashHand : IHand
    {
        readonly HandSuccessChecker handSuccessChecker;

        public HandName Name { get; }
        public int Score { get; }

        internal FlashHand(IPieceStacksProvider pieceStacksProvider, int score)
        {
            handSuccessChecker = new HandSuccessChecker(pieceStacksProvider);
            Name = ToFlash(pieceStacksProvider.HandName);
            Score = score;
        }

        public int GetNumberOfSuccesses(IEnumerable<IReadOnlyPiece> holdingPieces)
        {
            IEnumerable<IReadOnlyPiece> blackPieces = holdingPieces.Where(piece => piece.Color == PieceColor.Black);
            if (handSuccessChecker.Check(blackPieces)) return 1;
            
            IEnumerable<IReadOnlyPiece> redPieces = holdingPieces.Where(piece => piece.Color == PieceColor.Red);
            if (handSuccessChecker.Check(redPieces)) return 1;

            return 0;
        }
    }
}