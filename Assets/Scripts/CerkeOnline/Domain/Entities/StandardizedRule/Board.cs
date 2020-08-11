using System;
using UniRx;
using UniRx.Async;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule
{
    internal class Board : IBoard
    {
        public int Width { get; }
        public int Height { get; }

        readonly PositionArrayAccessor<IPiece> pieces;
        readonly IFieldEffectChecker fieldEffectChecker;
        readonly IPieceMoveTransactionFactory pieceMoveActionFactory;
        readonly PieceMoveVerifier moveVerifier;

        public IObservable<Unit> OnEveruValueChanged => onEveryValueChanged;
        readonly Subject<Unit> onEveryValueChanged = new Subject<Unit>();

        //これ、Boardが保持すべき情報ではない
        //ターン管理をここでするな！
        readonly OperationStatus operationStatus = new OperationStatus();

        internal Board(PositionArrayAccessor<IPiece> pieceMap, FieldEffectChecker fieldEffectChecker, IPieceMoveTransactionFactory pieceMoveActionFactory)
        {
            this.pieces = pieceMap;
            this.fieldEffectChecker = fieldEffectChecker;
            this.pieceMoveActionFactory = pieceMoveActionFactory;
            this.moveVerifier = new PieceMoveVerifier(pieces);

            Width = pieces.Width;
            Height = pieces.Height;
            
            onEveryValueChanged.OnNext(Unit.Default);
        }

        public IReadOnlyPiece GetPiece(PublicDataType.IntegerVector2 position)
        {
            if (!IsOnBoard(position))
                return null;

            return pieces.Read(position);
        }

        public IReadOnlyPiece SearchPiece(PieceName pieceName)
        {
            for (int i = 0; i < pieces.Width; i++)
            {
                for (int j = 0; j < pieces.Height; j++)
                {
                    IReadOnlyPiece piece = pieces.Read(new PublicDataType.IntegerVector2(i, j));
                    if (piece != null && piece.Name == pieceName)
                        return piece;
                }
            }

            return null;
        }

        bool isLocked = false;
        public async UniTask<PieceMoveResult> MovePiece(PublicDataType.IntegerVector2 startPosition,
            PublicDataType.IntegerVector2 viaPosition, 
            PublicDataType.IntegerVector2 endPosition,
            IPlayer player, IValueInputProvider<int> valueProvider)
        {
            if (isLocked) return new PieceMoveResult(false, false, null);

            var verifiedMove = moveVerifier.VerifyMove(player, startPosition, viaPosition, endPosition);
            if(verifiedMove == VerifiedMove.InvalidMove)
                return new PieceMoveResult(false, false, null);


            //1ターンに複数回動作する駒のためのロジック
            //ターン管理をここでするな！
            var movingPiece = verifiedMove.MovingPiece;
            if (movingPiece == operationStatus.PreviousPiece)
            {
                operationStatus.AddCount();
            }
            else
            {
                if (operationStatus.PreviousPiece != null)
                {
                    return new PieceMoveResult(false, false, null);
                }
                else
                {
                    operationStatus.Reset(movingPiece);
                }
            }
            bool isTurnEnd = operationStatus.Count >= movingPiece.NumberOfMoves;
            if (isTurnEnd)
                operationStatus.Reset(null);

            isLocked = true;
            IPieceMoveTransaction pieceMoveAction = pieceMoveActionFactory.Create(player, verifiedMove, pieces, fieldEffectChecker, valueProvider, isTurnEnd);

            PieceMoveResult result = await pieceMoveAction.StartMove();
            if (result.isSuccess)
                onEveryValueChanged.OnNext(Unit.Default);
            isLocked = false;

            return result;
        }

        public UniTask<PieceMoveResult> MovePiece(PublicDataType.IntegerVector2 startPosition, PublicDataType.IntegerVector2 lastPosition, IPlayer player, IValueInputProvider<int> valueProvider)
            => MovePiece(startPosition, lastPosition, lastPosition, player, valueProvider);
            
        
        public bool SetPiece(PublicDataType.IntegerVector2 position, IPiece piece)
        {
            if (!IsOnBoard(position) || pieces.Read(position) != null)
                return false;

            if (piece.Position != new PublicDataType.IntegerVector2(-1, -1))
                return false;

            piece.SetOnBoard(position);
            pieces.Write(position, piece);
            onEveryValueChanged.OnNext(Unit.Default);
            return true;
        }

        public bool IsOnBoard(PublicDataType.IntegerVector2 position)
        {
            return position.x >= 0 && position.y >= 0 && position.x < pieces.Width && position.y < pieces.Height;
        }
    }
}