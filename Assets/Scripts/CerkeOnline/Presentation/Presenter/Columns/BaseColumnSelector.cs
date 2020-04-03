using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Columns
{
    public abstract class BaseColumnSelector : MonoBehaviour
    {
        [SerializeField] IReadOnlyPieceUnityEvent onPieceSelected = default;
        [SerializeField] IntegerVector2UnityEvent onViaPositionSelected = default;
        [SerializeField] IntegerVector2UnityEvent onTargetPositionSelected = default;

        static readonly IntegerVector2 NonePosition = new IntegerVector2(-1, -1);
        IntegerVector2 startPosition = NonePosition;
        IntegerVector2 viaPosition = NonePosition;
        protected bool isLockSelecting = false;

        public void OnClickColumn(IntegerVector2 position)
        {
            if (isLockSelecting || this.startPosition == NonePosition || position == NonePosition)
            {
                this.startPosition = position;
                this.viaPosition = NonePosition;

                CallPieceSelectedEvent(position);
                
                return;
            }

            if (this.viaPosition == NonePosition)
            {
                this.viaPosition = position;
                CallViaPositionSelectedEvent(position);
                return;
            }

            onTargetPositionSelected.Invoke(position);
            OnColumnSelected(this.startPosition, this.viaPosition, position);
            this.startPosition = NonePosition;
        }

        protected abstract void OnColumnSelected(IntegerVector2 start, IntegerVector2 via, IntegerVector2 last);

        void CallPieceSelectedEvent(IntegerVector2 position)
        {
            var game = Application.GameController.Instance.Game;
            var board = game.Board;
            var piece = board.GetPiece(position);
            onPieceSelected.Invoke(piece);
        }
        
        void CallViaPositionSelectedEvent(IntegerVector2 position)
        {
            var game = Application.GameController.Instance.Game;
            var board = game.Board;
            var piece = board.GetPiece(position);
            if (piece != null)  //駒が無ければ経由点ではないので呼ばない
                onViaPositionSelected.Invoke(viaPosition);
        }
    }

    [System.Serializable] public class IntegerVector2UnityEvent : UnityEngine.Events.UnityEvent<IntegerVector2> { }
}