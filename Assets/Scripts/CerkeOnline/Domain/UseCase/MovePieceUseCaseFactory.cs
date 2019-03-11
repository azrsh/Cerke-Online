using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.Entities;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public static class MovePieceUseCaseFactory
    {
        public static IMovePieceUseCase Create(FirstOrSecond firstOrSecond, IValueInputProvider<int> valueProvider)
        {
            IGame game = GameController.Instance.Game;
            IPlayer player = game.GetPlayer(firstOrSecond);
            return new PlayerMovePieceUseCase(game, player, valueProvider);
        }
    }
}