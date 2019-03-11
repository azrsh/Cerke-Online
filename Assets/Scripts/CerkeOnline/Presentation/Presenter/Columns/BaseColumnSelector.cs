using UnityEngine;
using Azarashi.CerkeOnline.Domain.UseCase;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Columns
{
    public abstract class BaseColumnSelector : MonoBehaviour
    {   
        readonly Vector2Int nonePosition = new Vector2Int(-1, -1);
        Vector2Int position = new Vector2Int(-1, -1);
        
        public void OnClickColumn(Vector2Int position)
        {
            if (this.position == nonePosition)
            {
                this.position = position;
                return;
            }

            OnColumnSelected(this.position, position);
            this.position = nonePosition;
        }

        protected abstract void OnColumnSelected(Vector2Int start, Vector2Int end);
    }
}