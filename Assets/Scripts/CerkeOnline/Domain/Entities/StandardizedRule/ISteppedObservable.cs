using System;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule
{
    internal interface ISteppedObservable
    {
        IObservable<Unit> OnStepped { get; }
    }

    internal interface ISteppedObserver
    {
        IObserver<Unit> OnSteppied { get; }
    }
}