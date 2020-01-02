using System;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities.Official
{
    public interface ISemorkoObservable
    {
        IObservable<Unit> OnSemorko { get; }
    }

    public interface ISemorkoObserver
    {
        IObserver<Unit> OnSurmounted { get; }
    }
}