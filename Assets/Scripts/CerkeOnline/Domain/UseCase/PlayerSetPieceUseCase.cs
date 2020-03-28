using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public class PlayerSetPieceUseCase : ISetPieceUseCase
    {
        readonly IGame game;
        readonly IPlayer player;
        readonly ILogger logger;

        public PlayerSetPieceUseCase(IGame game, IPlayer player, ILogger logger)
        {
            this.game = game;
            this.player = player;
            this.logger = logger;
        }

        public void RequestToSetPiece(IntegerVector2 position, IPiece piece)
        {
            if (game.CurrentPlayer != player)
            {
                logger.Log("あなたのターンではありません.");
                return;
            }

            if (piece == null)
            {
                logger.Log("駒が選択されていません.");
                return;
            }

            IBoard board = game.Board;
            logger.Log(piece.PieceName.ToString() + "を" + position + "に配置.");
            bool result = board.SetPiece(position, piece);
            if (!result) logger.Log("駒の設置に失敗しました.");
            else
            {
                player.UseCapturedPiece(piece);
                game.TurnEnd();
            }
        }
    }
}