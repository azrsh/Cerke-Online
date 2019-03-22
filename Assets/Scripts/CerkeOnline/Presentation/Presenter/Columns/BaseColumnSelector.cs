using UnityEngine;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Columns
{
    public abstract class BaseColumnSelector : MonoBehaviour
    {   
        static readonly Vector2Int NonePosition = new Vector2Int(-1, -1);
        Vector2Int position = NonePosition;
        protected bool isLockSelecting = false;

        public void OnClickColumn(Vector2Int position)
        {
            if (isLockSelecting || this.position == NonePosition || position == NonePosition)
            {
                this.position = position;
                return;
            }

            OnColumnSelected(this.position, position);
            this.position = NonePosition;
        }

        protected abstract void OnColumnSelected(Vector2Int start, Vector2Int end);
    }
}