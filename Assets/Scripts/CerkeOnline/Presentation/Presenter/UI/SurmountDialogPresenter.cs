using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SurmountDialogPresenter : MonoBehaviour
{
    [SerializeField] GameObject surmountDialog = default;
    [SerializeField] Button surmountButton = default;
    [SerializeField] Button pickUpButton = default;

    enum PickUpOrSurmount
    {
        PickUp,Surmount
    };

    void Start()
    {
        surmountButton.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(_ => Close());
        pickUpButton.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(_ => Close());
        //Open(subject = new Subject<PickUpOrSurmount>());
    }

    void Close()
    {
        surmountDialog.SetActive(false);
    }

    void Open(IObserver<PickUpOrSurmount> observer)
    {
        surmountDialog.SetActive(true);
        IObservable<Unit> surmountAsObservable = surmountButton.OnClickAsObservable().TakeUntilDestroy(this);
        IObservable<Unit> pickUpAsObservable = pickUpButton.OnClickAsObservable().TakeUntilDestroy(this);
        surmountAsObservable.TakeUntil(pickUpAsObservable).First().Subscribe(_ => observer.OnNext(PickUpOrSurmount.Surmount));
        pickUpAsObservable.TakeUntil(surmountAsObservable).First().Subscribe(_ => observer.OnNext(PickUpOrSurmount.PickUp));
    }
}
