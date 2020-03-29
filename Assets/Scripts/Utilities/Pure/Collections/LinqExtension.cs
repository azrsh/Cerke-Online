using System.Linq;
using System.Collections.Generic;

namespace Azarashi.Utilities.Collections
{
    public static class LinqExtension
    {
        public static bool SequenceMatch<T>(this IEnumerable<T> enumerable1, IEnumerable<T> enumerable2)
            => !enumerable1.Except(enumerable2).Any() && !enumerable2.Except(enumerable1).Any();
    }
}