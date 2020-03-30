using System.Linq;
using System.Collections.Generic;

namespace Azarashi.Utilities.Collections
{
    public static class LinqExtension
    {
        /// <summary>
        /// 重複のないIEnumerable<T>の要素が一致するか確認します. 重複がある場合は, 含まれる要素の種類が一致していればtrueを返却します.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable1"></param>
        /// <param name="enumerable2"></param>
        /// <returns></returns>
        public static bool SequenceMatch<T>(this IEnumerable<T> enumerable1, IEnumerable<T> enumerable2)
            => !enumerable1.Except(enumerable2).Any() && !enumerable2.Except(enumerable1).Any();
    }
}