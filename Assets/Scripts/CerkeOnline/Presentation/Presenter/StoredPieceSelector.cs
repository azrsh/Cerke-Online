using UnityEngine;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Presentation.Presenter.Columns;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    [RequireComponent(typeof(BaseColumnSelector))]
    public class StoredPieceSelector : MonoBehaviour
    {
        static readonly Vector2Int NonePosition = new Vector2Int(-1, -1);

        IPiece selectedPiece;
        
        public void OnClickColumn(Vector2Int position)
        {
            IGame game = GameController.Instance.Game;
            if (game == null || selectedPiece == null)
                return;

            if (position != NonePosition && game.CurrentPlayer == selectedPiece.Owner)
                SetPieceUseCaseFactory.Create(game.CurrentTurn).RequestToSetPiece(position, selectedPiece);
            
            selectedPiece = null;
        }

        //Unsafe
        public void OnStoredPieceClicked(IReadOnlyPiece piece)
        {
            if(piece is IPiece)
                selectedPiece = (IPiece)piece;
        }
    }
}