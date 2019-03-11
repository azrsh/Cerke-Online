using System;
using Azarashi.CerkeOnline.Networking.Server;
namespace Azarashi.CerkeOnline.Networking
{
    public interface ICallbackRequestable<out T>
    {
        void InitializeOnServer(IReadOnlyServerContainer serverContainer);
        void InitializeOnClient(string clientId);
        void Request(Action<T> callback);
    }
}