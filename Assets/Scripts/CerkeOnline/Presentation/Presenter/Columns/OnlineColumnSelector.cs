using UnityEngine;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Networking.DataStructure;
using Azarashi.CerkeOnline.Domain.Entities;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Columns
{
    public class OnlineColumnSelector : BaseColumnSelector
    {
        // 現在はColumnSelectorを継承しているが, ColumnSelectorの内部処理は分離してPureC#にした方がいいかも
        
        protected override void OnColumnSelected(Vector2Int start, Vector2Int end)
        {
            IGame game = GameController.Instance.Game;
            if (game == null)
                return;

            PieceName pieceName = game.Board.GetPiece(start).PieceName;
            PieceMoveData pieceMoveData = new PieceMoveData(string.Empty, start, pieceName, end);
            GameController.Instance.ServerDelegate.PostMoveData(pieceMoveData);
        }
    }
}