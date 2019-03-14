using System.Collections.Generic;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    /// <summary>
    /// 役のinterface.
    /// </summary>
    public interface IHand
    {
        string Name { get; }
        int Score { get; }
        bool IsSccess(IReadOnlyList<IReadOnlyPiece> pieces);
    }
}