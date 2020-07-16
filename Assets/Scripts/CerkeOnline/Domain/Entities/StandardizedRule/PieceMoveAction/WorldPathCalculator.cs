using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.DataStructure;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction
{
    //interface, straightPath, brockenPathでクラスを分けたい
    internal static class WorldPathCalculator
    {
        /// <summary>
        /// 返却されるPathには開始地点は含まれない。
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="viaPosition"></param>
        /// <param name="endPosition"></param>
        /// <param name="pieces"></param>
        /// <param name="start2ViaPieceMovement"></param>
        /// <param name="via2EndPieceMovement"></param>
        /// <returns></returns>
        public static LinkedList<ColumnData> CalculatePath(PublicDataType.IntegerVector2 startPosition, PublicDataType.IntegerVector2 viaPosition, PublicDataType.IntegerVector2 endPosition, 
            PositionArrayAccessor<IPiece> pieces, PieceMovement start2ViaPieceMovement, PieceMovement via2EndPieceMovement)
        {
            //startPositionは各直線ではなく全体の開始地点でなければならない
            //インスタンス変数化？
            bool isFrontPlayersPiece = IsFrontPlayersPiece(pieces, startPosition);

            if (viaPosition == endPosition) return CalculateStraightPath(startPosition, endPosition, pieces, start2ViaPieceMovement, isFrontPlayersPiece);
            return CalculateBrockenPath(startPosition, viaPosition, endPosition, pieces, start2ViaPieceMovement, via2EndPieceMovement, isFrontPlayersPiece);
        }

        static LinkedList<ColumnData> CalculateBrockenPath(PublicDataType.IntegerVector2 startPosition, PublicDataType.IntegerVector2 viaPosition, PublicDataType.IntegerVector2 endPosition,
            PositionArrayAccessor<IPiece> pieces, PieceMovement start2ViaPieceMovement, PieceMovement via2EndPieceMovement, bool isFrontPlayersPiece)
        {
            var start2ViaPath = CalculateStraightPath(startPosition, viaPosition, pieces, start2ViaPieceMovement, isFrontPlayersPiece);
            var via2EndPath = CalculateStraightPath(viaPosition, endPosition, pieces, via2EndPieceMovement, isFrontPlayersPiece);

            var tempPath = start2ViaPath.Concat(via2EndPath);   //順序は保証されていないにも関わらず保証されたものとして利用
            return new LinkedList<ColumnData>(tempPath);
        }

        static LinkedList<ColumnData> CalculateStraightPath(PublicDataType.IntegerVector2 startPosition, PublicDataType.IntegerVector2 endPosition, 
            PositionArrayAccessor<IPiece> pieces, PieceMovement pieceMovement, bool isFrontPlayersPiece)
        {
            var relativeStart2EndPath = GetPath(startPosition, endPosition, isFrontPlayersPiece, pieceMovement);
            //順序は保証されていないにも関わらず保証されたものとして利用
            var worldPath = relativeStart2EndPath.Select(value => startPosition + value * (isFrontPlayersPiece ? -1 : 1));
            return new LinkedList<ColumnData>(worldPath.Select(value => new ColumnData(value, pieces)));
        }

        static bool IsFrontPlayersPiece(PositionArrayAccessor<IPiece> pieces, PublicDataType.IntegerVector2 startPosition) 
            => pieces.Read(startPosition).Owner != null && pieces.Read(startPosition).Owner.Encampment == Encampment.Front;

        static IEnumerable<PublicDataType.IntegerVector2> GetPath(PublicDataType.IntegerVector2 from, PublicDataType.IntegerVector2 to, bool isFrontPlayersPiece, PieceMovement pieceMovement)
        {
            PublicDataType.IntegerVector2 relativePosition = (to - from) * (isFrontPlayersPiece ? -1 : 1);
            return pieceMovement.GetPath(relativePosition) ?? throw new ArgumentException("移動不可能な移動先が指定されました.");
        }

    }
}
