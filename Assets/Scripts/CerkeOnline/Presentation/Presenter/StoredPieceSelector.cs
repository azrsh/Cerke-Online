using UnityEngine;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using Azarashi.CerkeOnline.Presentation.Presenter.Columns;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    [RequireComponent(typeof(BaseColumnSelector))]
    public class StoredPieceSelector : MonoBehaviour
    {
        static readonly IntegerVector2 NonePosition = new IntegerVector2(-1, -1);

        IPiece selectedPiece;
        
        public void OnClickColumn(IntegerVector2 position)
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