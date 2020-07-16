using UniRx.Async;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public class ConstantProvider : IValueInputProvider<int>
    {
        readonly int value;

        public ConstantProvider(int value)
        {
            this.value = value;
        }

        public UniTask<int> RequestValue()
        {
            return new UniTask<int>(value);
        }
    }
}