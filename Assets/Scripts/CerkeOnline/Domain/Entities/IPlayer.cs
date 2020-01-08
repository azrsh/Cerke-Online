
namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IPlayer : IReadOnlyPlayer
    {
        /// <summary>
        /// Playerに駒を取得させる.
        /// </summary>
        /// <param name="piece"></param>
        void GivePiece(IPiece piece);

        /// <summary>
        /// Playerが盤外に所持している駒を取りだす.
        /// </summary>
        /// <param name="piece"></param>
        void PickOut(IPiece piece);
    }
}