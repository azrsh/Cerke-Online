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
        readonly HandSuccessChecker handSuccessChecker;

        public string Name { get; }
        public int Score { get; }

        public LaDejixeceHand(IPieceStacksProvider pieceStacksProvider, int score)
        {
            this.pieceStacksProvider = pieceStacksProvider;
            handSuccessChecker = new HandSuccessChecker(pieceStacksProvider);
            Name = "同色" + HandNameDictionary.PascalToJapanese[pieceStacksProvider.GetType().Name]; //TODO べた書きの排除
            Score = score;
        }

        public int GetNumberOfSuccesses(IReadOnlyList<IReadOnlyPiece> holdingPieces)
        {
            IEnumerable<IReadOnlyPiece> blackPieces = holdingPieces.Where(piece => piece.Color == PieceColor.Black);
            if (handSuccessChecker.Check(blackPieces)) return 1;
            
            IEnumerable<IReadOnlyPiece> redPieces = holdingPieces.Where(piece => piece.Color == PieceColor.Red);
            if (handSuccessChecker.Check(redPieces)) return 1;

            return 0;
        }
    }
}