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
                logger.Log(new NotYourTurnMessage());
                return;
            }

            if (piece == null)
            {
                logger.Log(new NoPieceSelectedMessage());
                return;
            }

            IBoard board = game.Board;
            logger.Log(new SetPieceMessage(player, piece, position));
            bool result = board.SetPiece(position, piece);
            if (!result) logger.Log(new SetPieceFailureMessage());
            else
            {
                player.UseCapturedPiece(piece);
                game.TurnEnd();
            }
        }
    }
}