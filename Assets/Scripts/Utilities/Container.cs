using System;
using System.Collections.Generic;

namespace Azarashi.Utilities
{
    /// <summary>
    /// 同じようなものをだれか作ってるはずなので, 将来的には置き換える.
    /// </summary>
    public interface IReadOnlyContainer
    {
        T GetInstance<T>();
    }

    /// <summary>
    /// 同じようなものをだれか作ってるはずなので, 将来的には置き換える.
    /// </summary>
    public interface IContainer : IReadOnlyContainer
    {
        bool SetInstance<T>(T instance, bool forceAdd = false);
    }

    /// <summary>
    /// 同じようなものをだれか作ってるはずなので, 将来的には置き換える.
    /// </summary>
    public class Container : IContainer
    {
        readonly Dictionary<Type, object> dictionary;

        public Container(int initialCapacity = 16)
        {
            dictionary = new Dictionary<Type, object>(initialCapacity);
        }
        
        public bool SetInstance<T>(T instance, bool forceAdd = false)
        {
            bool contains = dictionary.ContainsKey(typeof(T));
            if (!forceAdd && contains) return false;
            if (forceAdd && contains) dictionary.Remove(typeof(T));

            dictionary.Add(typeof(T), instance);
            return true;
        }

        //Type Unsafe
        public T GetInstance<T>()
        {
            object instance;
            dictionary.TryGetValue(typeof(T), out instance);
            return (T)instance;
        }
    }
}