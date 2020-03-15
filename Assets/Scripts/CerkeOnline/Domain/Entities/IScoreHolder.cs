using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IScoreHolder
    {
        int MoveScore(IPlayer scorer, int score);
        bool TryGetScore(IPlayer player, out IntReactiveProperty score);
        bool Contains(IPlayer player);
        IReadOnlyReactiveProperty<int> GetScore(IPlayer player);
    }
}