using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Azarashi.Utilities.Collections;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;
using Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.DataStructure;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction
{
    //interface, straightPath, brockenPathでクラスを分けたい
    public class PieceMovePathCalculator
    {
        public LinkedList<ColumnData> CalculatePath(Vector2Int startPosition, Vector2Int viaPosition, Vector2Int endPosition, 
            Vector2YXArrayAccessor<IPiece> pieces, PieceMovement start2ViaPieceMovement, PieceMovement via2EndPieceMovement)
        {
            if (viaPosition == endPosition) return CalculateStraightPath(startPosition, endPosition, pieces, start2ViaPieceMovement);
            return CalculateBrockenPath(startPosition, viaPosition, endPosition, pieces, start2ViaPieceMovement, via2EndPieceMovement);
        }

        LinkedList<ColumnData> CalculateBrockenPath(Vector2Int startPosition, Vector2Int viaPosition, Vector2Int endPosition,
            Vector2YXArrayAccessor<IPiece> pieces, PieceMovement start2ViaPieceMovement, PieceMovement via2EndPieceMovement)
        {
            var start2ViaPath = CalculateStraightPath(startPosition, viaPosition, pieces, start2ViaPieceMovement);
            var via2EndPath = CalculateStraightPath(viaPosition, endPosition, pieces, via2EndPieceMovement);

            //順序は保証されていないにも関わらず保証されたものとして利用
            var tempPath = start2ViaPath.Union(via2EndPath).ToList();   
            return new LinkedList<ColumnData>(tempPath);
        }

        LinkedList<ColumnData> CalculateStraightPath(Vector2Int startPosition, Vector2Int endPosition, 
            Vector2YXArrayAccessor<IPiece> pieces, PieceMovement pieceMovement)
        {
            bool isFrontPlayersPiece = IsFrontPlayersPiece(pieces, startPosition);

            var relativeStart2EndPath = GetPath(startPosition, endPosition, isFrontPlayersPiece, pieceMovement);
            //順序は保証されていないにも関わらず保証されたものとして利用
            var worldPath = relativeStart2EndPath.Select(value => startPosition + value * (isFrontPlayersPiece ? -1 : 1));
            return new LinkedList<ColumnData>(worldPath.Select(value => new ColumnData(value, pieces)));
        }

        bool IsFrontPlayersPiece(Vector2YXArrayAccessor<IPiece> pieces, Vector2Int startPosition) => pieces.Read(startPosition).Owner != null && pieces.Read(startPosition).Owner.Encampment == Encampment.Front;

        IReadOnlyList<Vector2Int> GetPath(Vector2Int from, Vector2Int to, bool isFrontPlayersPiece, PieceMovement pieceMovement)
        {
            Vector2Int relativePosition = (to - from) * (isFrontPlayersPiece ? -1 : 1);
            return pieceMovement.GetPath(relativePosition) ?? throw new ArgumentException("移動不可能な移動先が指定されました.");
        }

    }
}
