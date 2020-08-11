using System;
using System.Collections.Generic;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Pieces
{
    internal class Minds : DefaultPiece, ISteppedObservable, ISteppedObserver
    {
        public override int NumberOfMoves => 2;

        IObservable<Unit> ISteppedObservable.OnStepped => onStepped;
        IObserver<Unit> ISteppedObserver.OnSteppied => onStepped;
        readonly Subject<Unit> onStepped = new Subject<Unit>();

        readonly PieceMovement[] normalPieceMovements;
        readonly PieceMovement[] expansionPieceMovements;

        public Minds(Terminologies.PieceColor color, PublicDataType.IntegerVector2 position, IPlayer owner, IExpandingMoveFieldChecker fieldChecker)
            : base(position, color, owner, Terminologies.PieceName.Minds, fieldChecker)
        {
            normalPieceMovements = new PieceMovement[]
            {
                new PieceMovement(new PublicDataType.IntegerVector2(0, 1), 1, false, 2), new PieceMovement(new PublicDataType.IntegerVector2(0, -1), 1, false, 2),
                new PieceMovement(new PublicDataType.IntegerVector2(1, 0), 1, false, 2), new PieceMovement(new PublicDataType.IntegerVector2(-1, 0), 1, false, 2),
                new PieceMovement(new PublicDataType.IntegerVector2(1, 1), 1, false, 2), new PieceMovement(new PublicDataType.IntegerVector2(1, -1), 1, false, 2),
                new PieceMovement(new PublicDataType.IntegerVector2(-1, 1), 1, false, 2), new PieceMovement(new PublicDataType.IntegerVector2(-1, -1), 1, false, 2)
            };
            expansionPieceMovements = normalPieceMovements;
        }

        public override IEnumerable<PieceMovement> GetMoveablePosition(bool isExpanded)
        {
            if (!isExpanded)
                return normalPieceMovements;

            return expansionPieceMovements;
        }

        public override void SetOwner(IPlayer owner) { }
        public override bool CaptureFromBoard() => false;
        public override bool IsOwner(IReadOnlyPlayer player) => true;
        public override bool IsCapturable() => false;
        public override bool CanLittuaWithoutJudge() => true;
        public override bool CanTakePiece() => false;
    }
}