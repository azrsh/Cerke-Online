using System;
using UniRx;
using UnityEngine;
using Azarashi.Utilities.Collections;
using Azarashi.CerkeOnline.Domain.Entities.Official.Pieces;
using Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.Official
{
    public class Board : IBoard
    {
        readonly Vector2YXArrayAccessor<IPiece> pieces;
        readonly IFieldEffectChecker fieldChecker;
        public IObservable<Unit> OnEveruValueChanged => onEveryValueChanged;
        readonly Subject<Unit> onEveryValueChanged = new Subject<Unit>();

        //これ、Boardが保持すべき情報ではない
        readonly OperationStatus operationStatus = new OperationStatus();

        public Board(Vector2YXArrayAccessor<IPiece> pieceMap, FieldEffectChecker fieldChecker)
        {
            this.pieces = pieceMap;
            this.fieldChecker = fieldChecker;
            
            onEveryValueChanged.OnNext(Unit.Default);
        }

        public IReadOnlyPiece GetPiece(Vector2Int position)
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
                    IReadOnlyPiece piece = pieces.Read(new Vector2Int(i, j));
                    if (piece != null && piece.PieceName == pieceName)
                        return piece;
                }
            }

            return null;
        }

        bool isLocked = false;
        public void MovePiece(Vector2Int startPosition, Vector2Int viaPosition, Vector2Int lastPosition, IPlayer player, IValueInputProvider<int> valueProvider, Action<PieceMoveResult> callback)
        {
            if (isLocked) return;

            if (!IsOnBoard(startPosition) || !IsOnBoard(lastPosition))
                throw new ArgumentException();

            bool areViaAndLastSame = viaPosition == lastPosition;
            IPiece movingPiece = pieces.Read(startPosition);
            IPiece viaPiece = pieces.Read(viaPosition);
            IPiece originalPiece = pieces.Read(lastPosition);     //元からある駒の意味で使っているが, 英語があってるか不明.
            bool isTargetNull = movingPiece == null;
            bool isViaPieceNull = viaPiece == null;//
            bool isOwner = !isTargetNull && movingPiece.IsOwner(player);
            bool isSameOwner = !isTargetNull && originalPiece != null && originalPiece.Owner == movingPiece.Owner;
            PieceMovement viaPieceMovement = PieceMovement.Default;
            PieceMovement lastPieceMovement = PieceMovement.Default;
            bool isMoveableToVia = !isTargetNull && movingPiece.TryToGetPieceMovement(viaPosition, out viaPieceMovement);//
            bool isMoveableToLast = !isTargetNull && (areViaAndLastSame || movingPiece.TryToGetPieceMovement(startPosition + lastPosition - viaPosition, out lastPieceMovement));//
            if (isTargetNull || (!areViaAndLastSame && isViaPieceNull) || !isOwner || isSameOwner || !isMoveableToVia || ! isMoveableToLast)//
            {
                callback(new PieceMoveResult(false, false, null));
                return;
            }

            //1ターンに複数回動作する駒のためのロジック
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
            IPieceMoveAction pieceMoveAction = null;
            if (areViaAndLastSame)
                pieceMoveAction = new PieceMoveAction.PieceMoveAction(player, startPosition, lastPosition, pieces, fieldChecker, valueProvider, viaPieceMovement, callback, () => onEveryValueChanged.OnNext(Unit.Default), isTurnEnd);
            else
                pieceMoveAction = new PieceSemorkoMoveAction(player, startPosition, viaPosition, lastPosition, pieces, fieldChecker, valueProvider, viaPieceMovement, lastPieceMovement, callback, () => onEveryValueChanged.OnNext(Unit.Default), isTurnEnd);
            
            pieceMoveAction.StartMove();
        }

        public void MovePiece(Vector2Int startPosition, Vector2Int lastPosition, IPlayer player, IValueInputProvider<int> valueProvider, Action<PieceMoveResult> callback)
            => MovePiece(startPosition, lastPosition, lastPosition, player, valueProvider, callback);
            
        
        public bool SetPiece(Vector2Int position, IPiece piece)
        {
            if (!IsOnBoard(position) || pieces.Read(position) != null)
                return false;

            if (piece.Position != new Vector2Int(-1, -1))
                return false;

            piece.SetOnBoard(position);
            pieces.Write(position, piece);
            onEveryValueChanged.OnNext(Unit.Default);
            return true;
        }

        public bool IsOnBoard(Vector2Int position)
        {
            return position.x >= 0 && position.y >= 0 && position.x < pieces.Width && position.y < pieces.Height;
        }
    }
}