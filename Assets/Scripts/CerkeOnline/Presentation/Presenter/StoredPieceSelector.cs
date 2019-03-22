using UnityEngine;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.UseCase;
using Azarashi.CerkeOnline.Presentation.Presenter.Columns;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    [RequireComponent(typeof(BaseColumnSelector))]
    public class StoredPieceSelector : MonoBehaviour
    {
        static readonly Vector2Int NonePosition = new Vector2Int(-1, -1);

        IPiece selectedPiece;

        void Start()
        {

        }

        public void OnClickColumn(Vector2Int position)
        {
            if (selectedPiece == null)
                return;

            IGame game = GameController.Instance.Game;
            if (position != NonePosition && game.CurrentPlayer == selectedPiece.Owner)
                SetPieceUseCaseFactory.Create(GameController.Instance.Game.CurrentTurn).RequestToSetPiece(position, selectedPiece);
            
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