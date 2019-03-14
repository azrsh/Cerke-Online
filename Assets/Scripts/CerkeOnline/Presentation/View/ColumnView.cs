using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;

namespace Azarashi.CerkeOnline.Presentation.View
{
    public class ColumnView : MonoBehaviour, IPointerClickHandler
    {
        readonly Subject<Unit> onClick = new Subject<Unit>();
        public IObservable<Unit> OnClick { get { return onClick; } }

        private void Start()
        {
            //GameController.Instance.Game.Board.
            //マスの色を取得して適用する
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick.OnNext(Unit.Default);
        }
    }
}