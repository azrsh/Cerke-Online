using System;
using System.Collections.Generic;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public class Player : IPlayer
    {
        readonly List<IPiece> pieces = new List<IPiece>();

        public IObservable<Unit> OnPieceStrageCahnged => onPieceStrageCahnged;
        readonly Subject<Unit> onPieceStrageCahnged = new Subject<Unit>();

        public IReadOnlyList<IReadOnlyPiece> GetPieceList()
        {
            return pieces;
        }

        public void GivePiece(IPiece piece)
        {
            if (piece != null && !pieces.Contains(piece))
            {
                pieces.Add(piece);
                onPieceStrageCahnged.OnNext(Unit.Default);
            }
        }

        public void PickOut(IPiece piece)
        {
            pieces.Remove(piece);
        }
    }
}