using UnityEngine;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Networking.DataStructure;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Columns
{
    public class OnlineColumnSelector : BaseColumnSelector
    {
        // 現在はColumnSelectorを継承しているが, ColumnSelectorの内部処理は分離してPureC#にした方がいいかも
        
        protected override void OnColumnSelected(Vector2Int start, Vector2Int end)
        {
            PieceName pieceName = GameController.Instance.Game.Board.GetPiece(start).PieceName;
            PieceMoveData pieceMoveData = new PieceMoveData(string.Empty, start, pieceName, end);
            GameController.Instance.ServerDelegate.PostMoveData(pieceMoveData);
        }
    }
}