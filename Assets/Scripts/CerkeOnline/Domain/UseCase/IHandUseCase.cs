using System.Collections.Generic;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public interface IHandUseCase
    {
        HandDifference GetHandDifference(IEnumerable<IHand> previousHands);

        IEnumerable<IHand> GetCurrentHands();
    }
}