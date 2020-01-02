using UnityEngine;
using Azarashi.Utilities.UnityEvents;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Columns
{
    public abstract class BaseColumnSelector : MonoBehaviour
    {
        [SerializeField] IReadOnlyPieceUnityEvent onPieceSelected = default;
        [SerializeField] Vector2IntUnityEvent onViaPositionSelected = default;
        [SerializeField] Vector2IntUnityEvent onTargetPositionSelected = default;

        static readonly Vector2Int NonePosition = new Vector2Int(-1, -1);
        Vector2Int startPosition = NonePosition;
        Vector2Int viaPosition = NonePosition;
        protected bool isLockSelecting = false;

        public void OnClickColumn(Vector2Int position)
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

        protected abstract void OnColumnSelected(Vector2Int start, Vector2Int via, Vector2Int last);

        void CallPieceSelectedEvent(Vector2Int position)
        {
            var game = Application.GameController.Instance.Game;
            var board = game.Board;
            var piece = board.GetPiece(position);
            onPieceSelected.Invoke(piece);
        }
        
        void CallViaPositionSelectedEvent(Vector2Int position)
        {
            var game = Application.GameController.Instance.Game;
            var board = game.Board;
            var piece = board.GetPiece(position);
            if (piece != null)  //駒が無ければ経由点ではないので呼ばない
                onViaPositionSelected.Invoke(viaPosition);
        }
    }
}