using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public class PlayerMovePieceUseCase : IMovePieceUseCase
    {
        readonly IGame game;
        readonly IPlayer player;
        readonly IValueInputProvider<int> inputProvider;

        public PlayerMovePieceUseCase(IGame game, IPlayer player, IValueInputProvider<int> inputProvider)
        {
            this.game = game;
            this.player = player;
            this.inputProvider = inputProvider;
        }

        public void RequestToMovePiece(Vector2Int startPosition, Vector2Int endPosition)
        {
            if (game.CurrentPlayer != player)
            {
                Debug.Log(" System > あなたのターンではありません.");
                return;
            }

            IBoard board = game.Board;
            if (board.GetPiece(startPosition) == null)
            {
                Debug.Log(" System > 駒が選択されていません.");
                return;

            }

            Debug.Log(" System > " + startPosition + " " + board.GetPiece(startPosition)?.PieceName.ToString() + "を" + endPosition + "へ移動");
            board.MovePiece(startPosition, endPosition, player, inputProvider, OnPieceMoved);
        }

        void OnPieceMoved(PieceMoveResult pieceMoveResult)
        {
            player.GivePiece(pieceMoveResult.gottenPiece);
            if (!pieceMoveResult.isSuccess) Debug.Log(" System > 駒の移動に失敗しました.");
            if (pieceMoveResult.isTurnEnd) game.OnTurnEnd();
        }
    }
}