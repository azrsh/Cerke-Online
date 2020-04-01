using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

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

        bool CommonCheck(IntegerVector2 startPosition)
        {
            if (game.CurrentPlayer != player)
            {
                logger.Log(new NotYourTurnMessage());
                return false;
            }

            var board = game.Board;
            var piece = board.GetPiece(startPosition);
            if (piece == null)
            {
                logger.Log(new NoPieceSelectedMessage());
                return false;
            }

            if (piece.Owner != null && piece.Owner != player)
            {
                logger.Log(new NotYourPieceMessage());
                return false;
            }

            return true;
        }

        public void RequestToMovePiece(IntegerVector2 startPosition, IntegerVector2 viaPosition, IntegerVector2 endPosition)
        {
            if (!CommonCheck(startPosition)) return;
            if (game.Board.GetPiece(viaPosition) == null)
            {
                logger.Log(new NoPieceViaPointMessage());
                return;
            }

            var board = game.Board;
            logger.Log(new PieceViaMovementMessage(player, board.GetPiece(startPosition), startPosition, viaPosition, endPosition));
            board.MovePiece(startPosition, viaPosition, endPosition, player, inputProvider, OnPieceMoved);
        }

        public void RequestToMovePiece(IntegerVector2 startPosition, IntegerVector2 endPosition)
        {
            if (!CommonCheck(startPosition)) return;

            var board = game.Board;
            logger.Log(new PieceMovementMessage(player, board.GetPiece(startPosition), startPosition, endPosition));
            board.MovePiece(startPosition, endPosition, player, inputProvider, OnPieceMoved);
        }

        void OnPieceMoved(PieceMoveResult pieceMoveResult)
        {
            player.GivePiece(pieceMoveResult.gottenPiece);
            if (!pieceMoveResult.isSuccess) logger.Log(new PieceMovementFailureMessage());
            if (pieceMoveResult.isTurnEnd) game.TurnEnd();    //ターン処理は移動する
        }
    }
}