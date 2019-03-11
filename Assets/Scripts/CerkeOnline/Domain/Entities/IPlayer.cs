using System;
using System.Collections.Generic;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IPlayer
    {
        IObservable<Unit> OnPieceStrageCahnged { get; }
        IReadOnlyList<IReadOnlyPiece> GetPieceList();
        void GivePiece(IPiece piece);
    }
}