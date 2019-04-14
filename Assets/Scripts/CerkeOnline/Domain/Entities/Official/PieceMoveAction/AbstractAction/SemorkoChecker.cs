using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.ActualAction;
using Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.DataStructure;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.PieceMoveAction.AbstractAction
{
    public class SemorkoChecker
    {
        readonly WaterEntryChecker waterEntryChecker;

        public SemorkoChecker(WaterEntryChecker waterEntryChecker)
        {
            this.waterEntryChecker = waterEntryChecker;
        }

        public bool CheckSemorko(Vector2Int viaPosition,IPlayer player, IPiece movingPiece, LinkedListNode<ColumnData> worldPathNode, Action semorkoAction, Action<IPiece> onFailure)
        {
            var nextPiece = worldPathNode.Value.Piece;
            var isViaPosition = worldPathNode.Value.Positin == viaPosition && worldPathNode.Next != null;
            if (!isViaPosition)
                return true;

            if (nextPiece == null  || semorkoAction == null)
            {
                onFailure(movingPiece);
                return false;
            }

            //Unsafe 踏み越えられた場合のイベント通知
            if (nextPiece is ISemorkoObserver)
                (nextPiece as ISemorkoObserver).OnSurmounted.OnNext(Unit.Default);

            if (waterEntryChecker.IsJudgmentNecessary(movingPiece, worldPathNode) ||
                waterEntryChecker.IsJudgmentNecessary(movingPiece, worldPathNode.Next))
                waterEntryChecker.JudgeWaterEntry(movingPiece, worldPathNode, semorkoAction);
            else
                semorkoAction();

            return false;
        }
    }
}