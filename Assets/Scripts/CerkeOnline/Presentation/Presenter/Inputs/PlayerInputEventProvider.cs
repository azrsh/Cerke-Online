using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Inputs
{
    public class PlayerInputEventProvider : MonoBehaviour, IInputEventProvider
    {
        bool userInputPermition = true;

        public IObservable<Unit> OnCommandButton { get { return onCommandButton; } }
        readonly Subject<Unit> onCommandButton = new Subject<Unit>();

        public IObservable<Unit> OnPauseButton { get { return onPauseButton; } }
        readonly Subject<Unit> onPauseButton = new Subject<Unit>();

        public IObservable<Unit> OnMouseClicked { get { return onMouseClicked; } }
        readonly Subject<Unit> onMouseClicked = new Subject<Unit>();

        void Start()
        {
            this.UpdateAsObservable().TakeUntilDestroy(this)
                .Where(_ => userInputPermition)
                .Where(_ => Input.GetKeyDown(KeyCode.Return))
                .Subscribe(onCommandButton.OnNext);
            this.UpdateAsObservable().TakeUntilDestroy(this)
                .Where(_ => userInputPermition)
                .Where(_ => Input.GetKeyDown(KeyCode.Escape))
                .Subscribe(onPauseButton.OnNext);
            this.UpdateAsObservable().TakeUntilDestroy(this)
                .Where(_ => userInputPermition)
                .Where(_ => Input.GetButtonDown("Fire1"))
                .Subscribe(onMouseClicked.OnNext);
        }

        public void PermitUserInput(bool value)
        {
            userInputPermition = value;
        }
    }
}