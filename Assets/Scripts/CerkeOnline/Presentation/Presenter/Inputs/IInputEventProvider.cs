using System;
using UniRx;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Inputs
{
    public interface IInputEventProvider
    {
        void PermitUserInput(bool value);
        IObservable<Unit> OnCommandButton { get; }
        IObservable<Unit> OnPauseButton { get; }
        IObservable<Unit> OnMouseClicked { get; }
    }
}