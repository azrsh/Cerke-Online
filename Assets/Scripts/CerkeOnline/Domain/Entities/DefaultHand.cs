using System.Collections.Generic;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    /// <summary>
    /// 通常役
    /// </summary>
    internal class DefaultHand : IHand
    {
        readonly HandSuccessChecker handSuccessChecker;

        public Terminologies.HandName Name { get; }
        public int Score { get; }

        internal DefaultHand(IPieceStacksProvider pieceStacksProvider, int score)
        {
            handSuccessChecker = new HandSuccessChecker(pieceStacksProvider);
            Name = pieceStacksProvider.HandName;
            Score = score;
        }

        public int GetNumberOfSuccesses(IEnumerable<IReadOnlyPiece> holdingPieces)
        {
            bool isSuccess = handSuccessChecker.Check(holdingPieces);
            return isSuccess ? 1 : 0;
        }
    }
}