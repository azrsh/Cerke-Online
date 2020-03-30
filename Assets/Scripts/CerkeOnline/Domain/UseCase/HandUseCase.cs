using System.Collections.Generic;
using System.Linq;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public class HandUsecase : IHandUseCase
    {
        readonly IPlayer player;
        readonly IHandDatabase handDatabase;

        public HandUsecase(IPlayer player, IHandDatabase handDatabase)
        {
            this.player = player;
            this.handDatabase = handDatabase;
        }

        public HandDifference GetHandDifference(IEnumerable<IHand> previousHands)
        {
            var currentHands = GetCurrentHands();
            return HandDifferenceCalculator.Calculate(previousHands, currentHands);
        }

        public IEnumerable<IHand> GetCurrentHands()
        {
            return handDatabase.SearchHands(player.GetPieceList());
        }
    }
}