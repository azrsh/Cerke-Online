using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Domain.UseCase
{
    public interface INewGameUseCase
    {
        IGame NewGame();
    }

    public class NewGameUseCase : INewGameUseCase
    {
        readonly Ruleset ruleset;
        readonly Terminologies.FirstOrSecond localPlayerFirstOrSecond;
        readonly Terminologies.Encampment localPlayerEncampment;

        public NewGameUseCase(Ruleset ruleset, Terminologies.FirstOrSecond localPlayerFirstOrSecond, Terminologies.Encampment localPlayerEncampment)
        {
            this.ruleset = ruleset;
            this.localPlayerFirstOrSecond = localPlayerFirstOrSecond;
            this.localPlayerEncampment = localPlayerEncampment;
        }

        public IGame NewGame()
        {
            var localPlayerEncampment = this.localPlayerEncampment;
            var remotePlayerEncampment = Terminologies.GetReversal(localPlayerEncampment);
            var firstPlayerEncampment = localPlayerFirstOrSecond == Terminologies.FirstOrSecond.First ? localPlayerEncampment : remotePlayerEncampment;
            return ruleset.Factory.CreateInstance(firstPlayerEncampment);
        }
    }

    public class NewNoteReplayGameUseCase : INewGameUseCase
    {
        readonly INoteRepository repository;
        readonly IGameInstanceFactory factory;

        public NewNoteReplayGameUseCase(INoteRepository repository, IGameInstanceFactory factory)
        {
            this.repository = repository;
            this.factory = factory;
        }

        public IGame NewGame()
        {
            var note = repository.GetNoteData();
            var firstPlayerEncampment = note.First == Terminologies.PieceColor.Black ? Terminologies.Encampment.Front : Terminologies.Encampment.Back;
            return factory.CreateInstance(firstPlayerEncampment);
        }
    }
}