using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;
using Azarashi.Utilities.Assertions;

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
        public static IEnumerable<IntegerVector2> CalculatePath(IntegerVector2 startPosition, IntegerVector2 viaPosition, IntegerVector2 endPosition,
            IReadOnlyPositionArrayAccessor<IReadOnlyPiece> pieces, PieceMovement start2ViaPieceMovement, PieceMovement via2EndPieceMovement)
        {
            //startPositionは各直線ではなく全体の開始地点でなければならない
            //インスタンス変数化？
            bool isFrontPlayersPiece = IsFrontPlayersPiece(pieces, startPosition);

            if (viaPosition == endPosition) return CalculateStraightPath(startPosition, endPosition, start2ViaPieceMovement, isFrontPlayersPiece);
            return CalculateBrockenPath(startPosition, viaPosition, endPosition, start2ViaPieceMovement, via2EndPieceMovement, isFrontPlayersPiece);
        }

        static IEnumerable<IntegerVector2> CalculateBrockenPath(IntegerVector2 startPosition, IntegerVector2 viaPosition, IntegerVector2 endPosition,
            PieceMovement start2ViaPieceMovement, PieceMovement via2EndPieceMovement, bool isFrontPlayersPiece)
        {
            var start2ViaPath = CalculateStraightPath(startPosition, viaPosition, start2ViaPieceMovement, isFrontPlayersPiece);
            var via2EndPath = CalculateStraightPath(viaPosition, endPosition, via2EndPieceMovement, isFrontPlayersPiece);

            var tempPath = start2ViaPath.Concat(via2EndPath);   //順序は保証されていないにも関わらず保証されたものとして利用
            return tempPath;
        }

        static IEnumerable<IntegerVector2> CalculateStraightPath(IntegerVector2 startPosition, IntegerVector2 endPosition, PieceMovement pieceMovement, bool isFrontPlayersPiece)
        {
            var relativeStart2EndPath = GetPath(startPosition, endPosition, isFrontPlayersPiece, pieceMovement);
            //順序は保証されていないにも関わらず保証されたものとして利用
            var worldPath = relativeStart2EndPath.Select(value => startPosition + value * (isFrontPlayersPiece ? -1 : 1));
            return worldPath;
        }

        static bool IsFrontPlayersPiece(IReadOnlyPositionArrayAccessor<IReadOnlyPiece> pieces, IntegerVector2 startPosition)
            => pieces.Read(startPosition).Owner != null && pieces.Read(startPosition).Owner.Encampment == Encampment.Front;

        static IEnumerable<IntegerVector2> GetPath(IntegerVector2 from, IntegerVector2 to, bool isFrontPlayersPiece, PieceMovement pieceMovement)
        {
            IntegerVector2 relativePosition = (to - from) * (isFrontPlayersPiece ? -1 : 1);
            var result = pieceMovement.GetPath(relativePosition);

            Assert.IsNotNull(result);

            return result;
        }

    }
}
