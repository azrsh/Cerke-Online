using System;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities.Official
{
    public interface ISurmountedObservable
    {
        IObservable<Unit> OnSurmounted { get; }
    }

    public interface ISemorkoObserver
    {
        IObserver<Unit> OnSurmounted { get; }
    }
}