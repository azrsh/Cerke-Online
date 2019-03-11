using System;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public class ConstantProvider : IValueInputProvider<int>
    {
        readonly int value;

        public ConstantProvider(int value)
        {
            this.value = value;
        }

        public void RequestValue(Action<int> callback)
        {
            callback(value);
        }
    }
}