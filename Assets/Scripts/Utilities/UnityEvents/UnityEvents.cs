using System;
using UnityEngine;
using UnityEngine.Events;

namespace Azarashi.Utilities.UnityEvents
{
    [Serializable] public class IntUnityEvent : UnityEvent<int> { }
    [Serializable] public class FloatUnityEvent : UnityEvent<float> { }
    [Serializable] public class BoolUnityEvent : UnityEvent<bool> { }
    [Serializable] public class StringUnityEvent : UnityEvent<string> { }
    [Serializable] public class Vector2IntUnityEvent : UnityEvent<Vector2Int> { }
}