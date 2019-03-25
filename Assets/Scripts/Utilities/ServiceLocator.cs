using System;
using System.Collections.Generic;

namespace Azarashi.Utilities
{
    /// <summary>
    /// 同じようなものをだれか作ってるはずなので, 将来的には置き換える.
    /// </summary>
    public interface IReadOnlyServiceLocator
    {
        T GetInstance<T>();
    }

    /// <summary>
    /// 同じようなものをだれか作ってるはずなので, 将来的には置き換える.
    /// </summary>
    public interface IServiceLocator : IReadOnlyServiceLocator
    {
        bool SetInstance<T>(T instance, bool forceAdd = false);
    }

    /// <summary>
    /// 同じようなものをだれか作ってるはずなので, 将来的には置き換える.
    /// </summary>
    public class DefaultServiceLocator : IServiceLocator
    {
        readonly Dictionary<Type, object> dictionary;

        public DefaultServiceLocator(int initialCapacity = 16)
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

    /// <summary>
    /// 同じようなものをだれか作ってるはずなので, 将来的には置き換える.
    /// </summary>
    public interface IServiceLocatorProvider
    {
        IServiceLocator ServiceLocator { get; }
    }
}