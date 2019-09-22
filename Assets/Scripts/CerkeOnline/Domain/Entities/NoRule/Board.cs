using System;
using UniRx;
using UnityEngine;
using Azarashi.Utilities.Collections;
using Azarashi.CerkeOnline.Domain.Entities.Official.Pieces;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.NoRule
{
    public class Board : IBoard
    {
        readonly Vector2YXArrayAccessor<IPiece> pieces;
        readonly Official.IFieldEffectChecker fieldChecker;
        public IObservable<Unit> OnEveruValueChanged => onEveryValueChanged;
        readonly Subject<Unit> onEveryValueChanged = new Subject<Unit>();

        //これ、Boardが保持すべき情報ではない
        readonly OperationStatus operationStatus = new OperationStatus();

        public Board(IPlayer frontPlayer, IPlayer backPlayer)
        {
            IPiece tam = new Tam(0, new Vector2Int(4, 4), null, null); //fieldEffectCheckerに渡すため別途生成

            FieldEffect[,] columns = new FieldEffect[,]
            {
                { FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal },
                { FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal },
                { FieldEffect.Normal, FieldEffect.Normal,  FieldEffect.Tarfe, FieldEffect.Normal, FieldEffect.Tammua, FieldEffect.Normal,  FieldEffect.Tarfe, FieldEffect.Normal, FieldEffect.Normal },
                { FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal,  FieldEffect.Tarfe, FieldEffect.Tammua,  FieldEffect.Tarfe, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal },
                { FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Tammua, FieldEffect.Tammua,  FieldEffect.Tanzo, FieldEffect.Tammua, FieldEffect.Tammua, FieldEffect.Normal, FieldEffect.Normal },
                { FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal,  FieldEffect.Tarfe, FieldEffect.Tammua,  FieldEffect.Tarfe, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal },
                { FieldEffect.Normal, FieldEffect.Normal,  FieldEffect.Tarfe, FieldEffect.Normal, FieldEffect.Tammua, FieldEffect.Normal,  FieldEffect.Tarfe, FieldEffect.Normal, FieldEffect.Normal },
                { FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal },
                { FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal, FieldEffect.Normal },
            };
            fieldChecker = new Official.FieldEffectChecker(new Vector2YXArrayAccessor<FieldEffect>(columns), tam);

            IPiece[,] pieces = new IPiece[,]
            {
                {    new Kua(0, new Vector2Int(0,0), backPlayer, fieldChecker),    new Dodor(0, new Vector2Int(1,0), backPlayer, fieldChecker),  new Vadyrd(0, new Vector2Int(2,0), backPlayer, fieldChecker),    new Varxle(0, new Vector2Int(3,0), backPlayer, fieldChecker),     new Ales(1, new Vector2Int(4,0), backPlayer, fieldChecker),    new Varxle(1, new Vector2Int(5,0), backPlayer, fieldChecker),  new Vadyrd(1, new Vector2Int(6,0), backPlayer, fieldChecker),    new Dodor(1, new Vector2Int(7,0), backPlayer, fieldChecker),     new Kua(1, new Vector2Int(8,0), backPlayer, fieldChecker) },
                { new Terlsk(1, new Vector2Int(0,1), backPlayer, fieldChecker),  new Gustuer(1, new Vector2Int(1,1), backPlayer, fieldChecker),                                                            null,  new Stistyst(1, new Vector2Int(3,1), backPlayer, fieldChecker),                                                             null,  new Stistyst(0, new Vector2Int(5,1), backPlayer, fieldChecker),                                                            null,  new Gustuer(1, new Vector2Int(7,1), backPlayer, fieldChecker),  new Terlsk(1, new Vector2Int(8,1), backPlayer, fieldChecker) },
                {  new Elmer(0, new Vector2Int(0,2), backPlayer, fieldChecker),    new Elmer(1, new Vector2Int(1,2), backPlayer, fieldChecker),   new Elmer(0, new Vector2Int(2,2), backPlayer, fieldChecker),     new Elmer(1, new Vector2Int(3,2), backPlayer, fieldChecker),  new Felkana(1, new Vector2Int(4,2), backPlayer, fieldChecker),     new Elmer(1, new Vector2Int(5,2), backPlayer, fieldChecker),   new Elmer(0, new Vector2Int(6,2), backPlayer, fieldChecker),    new Elmer(1, new Vector2Int(7,2), backPlayer, fieldChecker),   new Elmer(0, new Vector2Int(8,2), backPlayer, fieldChecker) },
                {                                                           null,                                                             null,                                                            null,                                                              null,                                                             null,                                                              null,                                                            null,                                                             null,                                                            null },
                {                                                           null,                                                             null,                                                            null,                                                              null,                                                              tam,                                                              null,                                                            null,                                                             null,                                                            null },
                {                                                           null,                                                             null,                                                            null,                                                              null,                                                             null,                                                              null,                                                            null,                                                             null,                                                            null },
                {   new Elmer(0, new Vector2Int(0,6), frontPlayer, fieldChecker),     new Elmer(1, new Vector2Int(1,6), frontPlayer, fieldChecker),    new Elmer(0, new Vector2Int(2,6), frontPlayer, fieldChecker),      new Elmer(1, new Vector2Int(3,6), frontPlayer, fieldChecker),   new Felkana(0, new Vector2Int(4,6), frontPlayer, fieldChecker),      new Elmer(1, new Vector2Int(5,6), frontPlayer, fieldChecker),    new Elmer(0, new Vector2Int(6,6), frontPlayer, fieldChecker),     new Elmer(1, new Vector2Int(7,6), frontPlayer, fieldChecker),    new Elmer(0, new Vector2Int(8,6), frontPlayer, fieldChecker) },
                {  new Terlsk(0, new Vector2Int(0,7), frontPlayer, fieldChecker),   new Gustuer(0, new Vector2Int(1,7), frontPlayer, fieldChecker),                                                            null,   new Stistyst(0, new Vector2Int(3,7), frontPlayer, fieldChecker),                                                             null,   new Stistyst(1, new Vector2Int(5,7), frontPlayer, fieldChecker),                                                            null,   new Gustuer(1, new Vector2Int(7,7), frontPlayer, fieldChecker),   new Terlsk(1, new Vector2Int(8,7), frontPlayer, fieldChecker) },
                {     new Kua(1, new Vector2Int(0,8), frontPlayer, fieldChecker),     new Dodor(1, new Vector2Int(1,8), frontPlayer, fieldChecker),   new Vadyrd(1, new Vector2Int(2,8), frontPlayer, fieldChecker),     new Varxle(1, new Vector2Int(3,8), frontPlayer, fieldChecker),      new Ales(0, new Vector2Int(4,8), frontPlayer, fieldChecker),     new Varxle(0, new Vector2Int(5,8), frontPlayer, fieldChecker),   new Vadyrd(0, new Vector2Int(6,8), frontPlayer, fieldChecker),     new Dodor(0, new Vector2Int(7,8), frontPlayer, fieldChecker),      new Kua(0, new Vector2Int(8,8), frontPlayer, fieldChecker) }
            };
            this.pieces = new Vector2YXArrayAccessor<IPiece>(pieces);

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

            IPiece movingPiece = pieces.Read(startPosition);
            IPiece viaPiece = pieces.Read(viaPosition);
            IPiece originalPiece = pieces.Read(lastPosition);     //元からある駒の意味で使っているが, 英語があってるか不明.
            bool isTargetNull = movingPiece == null;
            bool isViaPieceNull = viaPosition == null;//
            bool isOwner = !isTargetNull && movingPiece.IsOwner(player);
            bool isSameOwner = !isTargetNull && originalPiece != null && originalPiece.Owner == movingPiece.Owner;
            PieceMovement viaPieceMovement = PieceMovement.Default;
            PieceMovement lastPieceMovement = PieceMovement.Default;
            bool isMoveableToVia = !isTargetNull && movingPiece.TryToGetPieceMovement(viaPosition, out viaPieceMovement);//
            bool isMoveableToLast = !isTargetNull && movingPiece.TryToGetPieceMovement(startPosition + lastPosition - viaPosition, out lastPieceMovement);//
            if (isTargetNull || isViaPieceNull || !isOwner || isSameOwner || !isMoveableToVia || !isMoveableToLast)//
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
            var pieceMoveAction =
                new Official.PieceMoveAction.PieceSemorkoMoveAction(player, startPosition, viaPosition, lastPosition, pieces, fieldChecker, new ConstantProvider(5), viaPieceMovement, lastPieceMovement, callback, () => onEveryValueChanged.OnNext(Unit.Default), isTurnEnd);
            pieceMoveAction.StartMove();
        }

        public void MovePiece(Vector2Int startPosition, Vector2Int endPosition, IPlayer player, IValueInputProvider<int> valueProvider, Action<PieceMoveResult> callback)
        {
            if (isLocked) return;

            if (!IsOnBoard(startPosition) || !IsOnBoard(endPosition))
                throw new ArgumentException();

            IPiece movingPiece = pieces.Read(startPosition);
            IPiece originalPiece = pieces.Read(endPosition);     //元からある駒の意味で使っているが, 英語があってるか不明.
            bool isTargetNull = movingPiece == null;
            bool isOwner = !isTargetNull && movingPiece.IsOwner(player);
            bool isSameOwner = !isTargetNull && originalPiece != null && originalPiece.Owner == movingPiece.Owner;
            PieceMovement pieceMovement = PieceMovement.Default;
            bool isMoveable = !isTargetNull && movingPiece.TryToGetPieceMovement(endPosition, out pieceMovement);
            if (isTargetNull || !isOwner || isSameOwner || !isMoveable)
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
            var pieceMoveAction = 
                new Official.PieceMoveAction.PieceMoveAction(player, startPosition, endPosition, pieces, fieldChecker, new ConstantProvider(5), pieceMovement, callback, () => onEveryValueChanged.OnNext(Unit.Default), isTurnEnd);
            pieceMoveAction.StartMove();
        }
        
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