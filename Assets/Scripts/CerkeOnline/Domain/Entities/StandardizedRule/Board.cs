using System;
using UniRx;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule
{
    internal class Board : IBoard
    {
        public int Width { get; }
        public int Height { get; }

        readonly PositionArrayAccessor<IPiece> pieces;
        readonly IFieldEffectChecker fieldChecker;
        readonly IPieceMoveActionFactory pieceMoveActionFactory;

        public IObservable<Unit> OnEveruValueChanged => onEveryValueChanged;
        readonly Subject<Unit> onEveryValueChanged = new Subject<Unit>();

        //これ、Boardが保持すべき情報ではない
        //ターン管理をここでするな！
        readonly OperationStatus operationStatus = new OperationStatus();

        internal Board(PositionArrayAccessor<IPiece> pieceMap, FieldEffectChecker fieldChecker, IPieceMoveActionFactory pieceMoveActionFactory)
        {
            this.pieces = pieceMap;
            this.fieldChecker = fieldChecker;
            this.pieceMoveActionFactory = pieceMoveActionFactory;

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
        public void MovePiece(PublicDataType.IntegerVector2 startPosition, PublicDataType.IntegerVector2 viaPosition, PublicDataType.IntegerVector2 endPosition, IPlayer player, IValueInputProvider<int> valueProvider, Action<PieceMoveResult> callback)
        {
            if (isLocked) return;

            if (!IsOnBoard(startPosition) || !IsOnBoard(endPosition))
                throw new ArgumentException();

            bool areViaAndLastSame = viaPosition == endPosition;
            IPiece movingPiece = pieces.Read(startPosition);
            IPiece viaPiece = pieces.Read(viaPosition);
            IPiece originalPiece = pieces.Read(endPosition);     //元からある駒の意味で使っているが, 英語があってるか不明.
            bool isTargetNull = movingPiece == null;
            bool isViaPieceNull = viaPiece == null;//
            bool isOwner = !isTargetNull && movingPiece.IsOwner(player);
            bool isSameOwner = !isTargetNull && originalPiece != null && originalPiece.Owner == movingPiece.Owner;
            PieceMovement start2ViaPieceMovement = PieceMovement.Default;
            PieceMovement via2EndPieceMovement = PieceMovement.Default;
            bool isMoveableToVia = !isTargetNull && movingPiece.TryToGetPieceMovement(viaPosition, out start2ViaPieceMovement);//
            bool isMoveableToLast = !isTargetNull && (areViaAndLastSame || movingPiece.TryToGetPieceMovement(startPosition + endPosition - viaPosition, out via2EndPieceMovement));//
            if (isTargetNull || (!areViaAndLastSame && isViaPieceNull) || !isOwner || isSameOwner || !isMoveableToVia || ! isMoveableToLast)//
            {
                callback(new PieceMoveResult(false, false, null));
                return;
            }

            //1ターンに複数回動作する駒のためのロジック
            //ターン管理をここでするな！
            if (movingPiece == operationStatus.PreviousPiece)
            {
                operationStatus.AddCount();
            }
            else
            {
                if (operationStatus.PreviousPiece != null)
                {
                    callback(new PieceMoveResult(false, false, null));
                    return;
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
            callback += (result) => { isLocked = false; };
            IPieceMoveAction pieceMoveAction = pieceMoveActionFactory.Create(player, startPosition, viaPosition, endPosition,
                            pieces, fieldChecker, valueProvider,
                            start2ViaPieceMovement, via2EndPieceMovement,
                            callback, () => onEveryValueChanged.OnNext(Unit.Default), isTurnEnd);
            pieceMoveAction.StartMove();
        }

        public void MovePiece(PublicDataType.IntegerVector2 startPosition, PublicDataType.IntegerVector2 lastPosition, IPlayer player, IValueInputProvider<int> valueProvider, Action<PieceMoveResult> callback)
            => MovePiece(startPosition, lastPosition, lastPosition, player, valueProvider, callback);
            
        
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