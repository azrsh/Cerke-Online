using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public class PlayerMovePieceUseCase : IMovePieceUseCase
    {
        readonly IGame game;
        readonly IPlayer player;
        readonly IValueInputProvider<int> inputProvider;
        readonly ILogger logger;

        public PlayerMovePieceUseCase(IGame game, IPlayer player, IValueInputProvider<int> inputProvider, ILogger logger)
        {
            this.game = game;
            this.player = player;
            this.inputProvider = inputProvider;
            this.logger = logger;
        }

        public void RequestToMovePiece(Vector2Int startPosition, Vector2Int endPosition)
        {
            if (game.CurrentPlayer != player)
            {
                logger.Log("あなたのターンではありません.");
                return;
            }

            var board = game.Board;
            var piece = board.GetPiece(startPosition);
            if (piece == null)
            {
                logger.Log("駒が選択されていません.");
                return;
            }

            if (piece.Owner != null && piece.Owner != player)
            {
                logger.Log("あなたの駒ではありません.");
                return;
            }

            logger.Log(startPosition + " " + board.GetPiece(startPosition)?.PieceName.ToString() + "を" + endPosition + "へ移動");
            board.MovePiece(startPosition, endPosition, player, inputProvider, OnPieceMoved);
        }

        void OnPieceMoved(PieceMoveResult pieceMoveResult)
        {
            player.GivePiece(pieceMoveResult.gottenPiece);
            if (!pieceMoveResult.isSuccess) logger.Log("駒の移動に失敗しました.");
            if (pieceMoveResult.isTurnEnd) game.OnTurnEnd();
        }
    }
}