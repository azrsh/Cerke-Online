using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Pieces
{
    public class Tam : DefaultPiece, ISurmountedObservable, ISurmountedObserver
    {
        public override int NumberOfMoves => 2;

        IObservable<Unit> ISurmountedObservable.OnSurmounted => onSurmounted;
        IObserver<Unit> ISurmountedObserver.OnSurmounted => onSurmounted;
        readonly Subject<Unit> onSurmounted = new Subject<Unit>();

        readonly PieceMovement[] normalPieceMovements;
        readonly PieceMovement[] expansionPieceMovements;

        public Tam(int color, Vector2Int position, IPlayer owner, IExpandingMoveFieldChecker fieldChecker) : base(position, color, owner, Terminologies.PieceName.Tam, fieldChecker)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new Vector2Int(0, 1), 1, false, 2), new PieceMovement(new Vector2Int(0, -1), 1, false, 2),
                new PieceMovement(new Vector2Int(1, 0), 1, false, 2), new PieceMovement(new Vector2Int(-1, 0), 1, false, 2),
                new PieceMovement(new Vector2Int(1, 1), 1, false, 2), new PieceMovement(new Vector2Int(1, -1), 1, false, 2),
                new PieceMovement(new Vector2Int(-1, 1), 1, false, 2), new PieceMovement(new Vector2Int(-1, -1), 1, false, 2)
            };
            expansionPieceMovements = normalPieceMovements;
        }

        public override IReadOnlyList<PieceMovement> GetMoveablePosition(bool isExpanded)
        {
            if (!isExpanded)
                return normalPieceMovements;

            return expansionPieceMovements;
        }

        public override void SetOwner(IPlayer owner) { }
        public override bool PickUpFromBoard() => false;
        public override bool IsOwner(IPlayer player) => true;
        public override bool IsPickupable() => false;
        public override bool CanLittuaWithoutJudge() => true;
        public override bool CanTakePiece() => false;
    }
}