using System;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule
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