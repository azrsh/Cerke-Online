using UnityEngine;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Columns
{
    public abstract class BaseColumnSelector : MonoBehaviour
    {
        [SerializeField] IReadOnlyPieceUnityEvent onPieceSelected = default;

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
                return;
            }

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
    }
}