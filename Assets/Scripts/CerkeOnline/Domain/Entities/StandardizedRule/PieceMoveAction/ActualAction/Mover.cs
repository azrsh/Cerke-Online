using System.Collections.Generic;
using System.Diagnostics;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.ActualAction
{
    internal class Mover
    {
        readonly PositionArrayAccessor<IPiece> pieceMap;
        
        public Mover(PositionArrayAccessor<IPiece> pieceMap)
        {
            this.pieceMap = pieceMap;
        }

        /// <summary>
        /// 指定された駒を空いたマスに移動する.
        /// </summary>
        /// <param name="movingPiece"></param>
        /// <param name="endWorldPosition"></param>
        /// <param name="isForceMove"></param>
        /// <returns></returns>
        public bool MovePiece(IPiece movingPiece, PublicDataType.IntegerVector2 endWorldPosition, bool isForceMove = false)
        {
            PublicDataType.IntegerVector2 startWorldPosition = movingPiece.Position;

            if (pieceMap.Read(endWorldPosition) != null)
                return false;

            if (!movingPiece.MoveTo(endWorldPosition, isForceMove))
                return false;

            //この順で書きまないと現在いる座標と同じ座標をendWorldPositionに指定されたとき盤上から駒の判定がなくなる
            pieceMap.Write(startWorldPosition, null);
            pieceMap.Write(endWorldPosition, movingPiece);
            return true;
        }
    }
}