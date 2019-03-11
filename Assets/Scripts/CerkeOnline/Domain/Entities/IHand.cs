using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    /// <summary>
    /// 役のinterface.
    /// </summary>
    public interface IHand
    {
        int Score { get; }
        bool IsSccess(IReadOnlyList<IPiece> peaces);
    }
}