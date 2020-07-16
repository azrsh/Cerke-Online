using System;
using UniRx.Async;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IValueInputProvider<T>
    {
        UniTask<T> RequestValue();
    }
}