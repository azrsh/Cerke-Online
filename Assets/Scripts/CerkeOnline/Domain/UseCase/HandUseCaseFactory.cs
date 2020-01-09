using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public static class HandUseCaseFactory
    {
        public static IHandUseCase Create(Terminologies.FirstOrSecond firstOrSecond)
        {
            var game = GameController.Instance.Game;
            var handDatabase = game.HandDatabase;
            if (game == null || handDatabase == null) return null;

            var player = game.GetPlayer(firstOrSecond);
            return new HandUsecase(player, handDatabase);
        }
    }
}