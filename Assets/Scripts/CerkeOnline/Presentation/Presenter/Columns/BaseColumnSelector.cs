using UnityEngine;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Columns
{
    public abstract class BaseColumnSelector : MonoBehaviour
    {   
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
    }
}