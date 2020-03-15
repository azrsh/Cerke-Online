using System;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule
{
    public interface ISteppedObservable
    {
        IObservable<Unit> OnStepped { get; }
    }

    public interface ISteppedObserver
    {
        IObserver<Unit> OnSteppied { get; }
    }
}