using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Application;

public class CurrentTurnPresenter : MonoBehaviour
{
    [SerializeField] Text currentTurnText = default;

    void Start()
    {
        IGame game = GameController.Instance.Game;
        this.UpdateAsObservable().TakeUntilDestroy(this)
            .Select(_ => game.CurrentTurn).DistinctUntilChanged().Subscribe(value =>
            {
                currentTurnText.text = "現在のターン : " + value.ToString();
                Debug.Log(currentTurnText.text);
            });
    }
}
