using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public class SeasonUseCase
    {
        readonly IGame game;

        public SeasonUseCase(IGame game)
        {
            this.game = game;
        }

        public void Next()
        {
            game.SeasonSequencer.StartNextSeason();
        }
    }
}