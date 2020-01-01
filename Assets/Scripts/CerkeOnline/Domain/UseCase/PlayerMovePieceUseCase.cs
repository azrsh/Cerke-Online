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

        bool CommonCheck(Vector2Int startPosition)
        {
            if (game.CurrentPlayer != player)
            {
                logger.Log("あなたのターンではありません.");
                return false;
            }

            var board = game.Board;
            var piece = board.GetPiece(startPosition);
            if (piece == null)
            {
                logger.Log("駒が選択されていません.");
                return false;
            }

            if (piece.Owner != null && piece.Owner != player)
            {
                logger.Log("あなたの駒ではありません.");
                return false;
            }

            return true;
        }

        public void RequestToMovePiece(Vector2Int startPosition, Vector2Int viaPosition, Vector2Int lastPosition)
        {
            if (!CommonCheck(startPosition)) return;
            if (game.Board.GetPiece(viaPosition) == null)
            {
                logger.Log("経由点に駒がありません");
                return;
            }

            var board = game.Board;
            logger.Log(startPosition + " " + board.GetPiece(startPosition)?.PieceName.ToString() + "を" + 
                        viaPosition + "を経由して" + 
                        lastPosition + "へ移動");
            board.MovePiece(startPosition, viaPosition, lastPosition, player, inputProvider, OnPieceMoved);
        }

        public void RequestToMovePiece(Vector2Int startPosition, Vector2Int lastPosition)
        {
            if (!CommonCheck(startPosition)) return;

            var board = game.Board;
            logger.Log(startPosition + " " + board.GetPiece(startPosition)?.PieceName.ToString() + "を" + lastPosition + "へ移動");
            board.MovePiece(startPosition, lastPosition, player, inputProvider, OnPieceMoved);
        }

        void OnPieceMoved(PieceMoveResult pieceMoveResult)
        {
            player.GivePiece(pieceMoveResult.gottenPiece);
            if (!pieceMoveResult.isSuccess) logger.Log("駒の移動に失敗しました.");
            if (pieceMoveResult.isTurnEnd) game.OnTurnEnd();    //ターン処理は移動する
        }
    }
}