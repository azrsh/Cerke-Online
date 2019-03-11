using System;

namespace Azarashi.CerkeOnline.Networking
{
    public interface IBroadcastable<T>
    {
        IObservable<T> OnRecieved { get; }
        void Broadcast(T data);
    }
}