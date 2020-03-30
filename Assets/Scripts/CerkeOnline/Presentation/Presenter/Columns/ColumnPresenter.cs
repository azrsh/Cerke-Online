using UnityEngine;
using UniRx;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using Azarashi.CerkeOnline.Presentation.View;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Columns
{
    [RequireComponent(typeof(IMapProvider<Transform>))]
    public class ColumnPresenter : MonoBehaviour
    {
        [SerializeField] IntegerVector2UnityEvent onColumnClicked = default;

        private void Start()
        {
            Transform[,] columns = GetComponent<IMapProvider<Transform>>().GetMap();

            for (int x = 0; x < columns.GetLength(0); x++)
            {
                for (int y = 0; y < columns.GetLength(1); y++)
                {
                    ColumnView columnView = columns[x, y].gameObject.AddComponent<ColumnView>();
                    IntegerVector2 columnPosition = new IntegerVector2(x, y);
                    columnView.OnClick.TakeUntilDestroy(this).Select(_ => columnPosition).Subscribe(onColumnClicked.Invoke);
                }
            }
        }
    }
}