using UnityEngine;
using UniRx;
using Azarashi.Utilities.UnityEvents;
using Azarashi.CerkeOnline.Presentation.View;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Columns
{
    [RequireComponent(typeof(IMapProvider<Transform>))]
    public class ColumnPresenter : MonoBehaviour
    {
        [SerializeField] Vector2IntUnityEvent onColumnClicked = default;

        private void Start()
        {
            Transform[,] columns = GetComponent<IMapProvider<Transform>>().GetMap();

            for (int i = 0; i < columns.GetLength(0); i++)
            {
                for (int j = 0; j < columns.GetLength(1); j++)
                {
                    ColumnView columnView = columns[i, j].gameObject.AddComponent<ColumnView>();
                    Vector2Int columnPosition = new Vector2Int(i, j);
                    columnView.OnClick.TakeUntilDestroy(this).Select(_ => columnPosition).Subscribe(onColumnClicked.Invoke);
                }
            }
        }
    }
}