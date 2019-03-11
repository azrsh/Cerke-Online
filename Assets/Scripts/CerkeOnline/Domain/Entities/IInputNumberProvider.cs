using System;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IValueInputProvider<T>
    {
        void RequestValue(Action<T> callback);
    }
}