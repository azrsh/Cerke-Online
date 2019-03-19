using System;
using UniRx;
using UnityEngine;
using Azarashi.Utilities.Array2D;
using Azarashi.CerkeOnline.Domain.Entities.Official.Pieces;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities.NoneRule
{
    public class Board : IBoard
    {
        readonly Vector2ArrayAccessor<IPiece> pieces;
        readonly Vector2ArrayAccessor<FieldEffect> columns;
        public IObservable<Unit> OnEveruValueChanged => onEveryValueChanged;
        readonly Subject<Unit> onEveryValueChanged = new Subject<Unit>();

        public Board(IPlayer firstPlayer, IPlayer secondPlayer)
        {
            IPiece[,] pieces = new IPiece[,]
            {
                {    new Kua(0, new Vector2Int(0,0), secondPlayer),    new Dodor(0, new Vector2Int(1,0), secondPlayer),  new Vadyrd(0, new Vector2Int(2,0), secondPlayer),    new Varxle(0, new Vector2Int(3,0), secondPlayer),     new Ales(1, new Vector2Int(4,0), secondPlayer),    new Varxle(1, new Vector2Int(5,0), secondPlayer),  new Vadyrd(1, new Vector2Int(6,0), secondPlayer),    new Dodor(1, new Vector2Int(7,0), secondPlayer),     new Kua(1, new Vector2Int(8,0), secondPlayer) },
                { new Terlsk(1, new Vector2Int(0,1), secondPlayer),  new Gustuer(1, new Vector2Int(1,1), secondPlayer),                                              null,  new Stistyst(1, new Vector2Int(3,1), secondPlayer),                                               null,  new Stistyst(0, new Vector2Int(5,1), secondPlayer),                                              null,  new Gustuer(1, new Vector2Int(7,1), secondPlayer),  new Terlsk(1, new Vector2Int(8,1), secondPlayer) },
                {  new Elmer(0, new Vector2Int(0,2), secondPlayer),    new Elmer(1, new Vector2Int(1,2), secondPlayer),   new Elmer(0, new Vector2Int(2,2), secondPlayer),     new Elmer(1, new Vector2Int(3,2), secondPlayer),  new Felkana(1, new Vector2Int(4,2), secondPlayer),     new Elmer(1, new Vector2Int(5,2), secondPlayer),   new Elmer(0, new Vector2Int(6,2), secondPlayer),    new Elmer(1, new Vector2Int(7,2), secondPlayer),   new Elmer(0, new Vector2Int(8,2), secondPlayer) },
                {                                             null,                                               null,                                              null,                                                null,                                               null,                                                null,                                              null,                                               null,                                              null },
                {                                             null,                                               null,                                              null,                                                null,              new Tam(0, new Vector2Int(4,4), null),                                                null,                                              null,                                               null,                                              null },
                {                                             null,                                               null,                                              null,                                                null,                                               null,                                                null,                                              null,                                               null,                                              null },
                {   new Elmer(0, new Vector2Int(0,6), firstPlayer),     new Elmer(1, new Vector2Int(1,6), firstPlayer),    new Elmer(0, new Vector2Int(2,6), firstPlayer),      new Elmer(1, new Vector2Int(3,6), firstPlayer),   new Felkana(0, new Vector2Int(4,6), firstPlayer),      new Elmer(1, new Vector2Int(5,6), firstPlayer),    new Elmer(0, new Vector2Int(6,6), firstPlayer),     new Elmer(1, new Vector2Int(7,6), firstPlayer),    new Elmer(0, new Vector2Int(8,6), firstPlayer) },
                {  new Terlsk(0, new Vector2Int(0,7), firstPlayer),   new Gustuer(0, new Vector2Int(1,7), firstPlayer),                                              null,   new Stistyst(0, new Vector2Int(3,7), firstPlayer),                                               null,   new Stistyst(1, new Vector2Int(5,7), firstPlayer),                                              null,   new Gustuer(1, new Vector2Int(7,7), firstPlayer),   new Terlsk(1, new Vector2Int(8,7), firstPlayer) },
                {     new Kua(1, new Vector2Int(0,8), firstPlayer),     new Dodor(1, new Vector2Int(1,8), firstPlayer),   new Vadyrd(1, new Vector2Int(2,8), firstPlayer),     new Varxle(1, new Vector2Int(3,8), firstPlayer),      new Ales(0, new Vector2Int(4,8), firstPlayer),     new Varxle(0, new Vector2Int(5,8), firstPlayer),   new Vadyrd(0, new Vector2Int(6,8), firstPlayer),     new Dodor(0, new Vector2Int(7,8), firstPlayer),      new Kua(0, new Vector2Int(8,8), firstPlayer) }
            };
            this.pieces = new Vector2ArrayAccessor<IPiece>(pieces);
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
            this.columns = new Vector2ArrayAccessor<FieldEffect>(columns);
            onEveryValueChanged.OnNext(Unit.Default);
        }

        public IReadOnlyPiece GetPiece(Vector2Int position)
        {
            if (!IsOnBoard(position))
                return null;

            return pieces.Read(position);
        }

        bool isLocked = false;
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

            isLocked = true;
            callback += (result) => { isLocked = false; };
            Official.PieceMoveAction pieceMoveAction = new Official.PieceMoveAction(startPosition, endPosition, pieces, columns, new ConstantProvider(5), pieceMovement, callback, () => onEveryValueChanged.OnNext(Unit.Default));
            pieceMoveAction.StartMove();
        }
        
        public bool SetPiece(Vector2Int position, IPiece piece)
        {
            if (!IsOnBoard(position) || pieces.Read(position) != null)
                return false;

            if (piece.Position == new Vector2Int(-1, -1))
                piece.SetOnBoard(position);

            pieces.Write(position, piece);
            onEveryValueChanged.OnNext(Unit.Default);
            return true;
        }
        
        bool IsOnBoard(Vector2Int position)
        {
            return position.x >= 0 && position.y >= 0 && position.x < pieces.Width && position.y < pieces.Height;
        }

        bool IsInWater(Vector2Int position)
        {
            return columns.Read(position) == FieldEffect.Tammua || columns.Read(position) == FieldEffect.Tanzo;
        }
    }
}