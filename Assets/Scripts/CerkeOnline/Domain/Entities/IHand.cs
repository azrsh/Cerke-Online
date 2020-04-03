using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    /// <summary>
    /// 役のinterface.
    /// </summary>
    public interface IHand
    {
        Terminologies.HandName Name { get; }
        int Score { get; }
        int GetNumberOfSuccesses(IEnumerable<IReadOnlyPiece> pieces);
    }
}