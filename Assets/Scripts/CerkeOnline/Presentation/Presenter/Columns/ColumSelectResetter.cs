using UnityEngine;
using UnityEngine.EventSystems;
using Azarashi.Utilities.UnityEvents;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Columns
{
    [RequireComponent(typeof(Collider2D))]
    public class ColumSelectResetter : MonoBehaviour, IPointerClickHandler
    {
        readonly Vector2Int nonePosition = new Vector2Int(-1, -1);
        [SerializeField] Vector2IntUnityEvent onOutOfBoardClicked = default;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            onOutOfBoardClicked.Invoke(nonePosition);
        }
    }
}