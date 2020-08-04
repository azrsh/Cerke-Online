using System.Collections.Generic;
using System.Diagnostics;

namespace Azarashi.Utilities.Assertions
{
    public static class Assert
    {
        [Conditional("UNITY_ASSERTIONS")] public static void AreApproximatelyEqual(float expected, float actual, float tolerance, string message) => UnityEngine.Assertions.Assert.AreApproximatelyEqual(expected, actual, tolerance, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreApproximatelyEqual(float expected, float actual, float tolerance) => UnityEngine.Assertions.Assert.AreApproximatelyEqual(expected, actual, tolerance);
        [Conditional("UNITY_ASSERTIONS")] public static void AreApproximatelyEqual(float expected, float actual) => UnityEngine.Assertions.Assert.AreApproximatelyEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreApproximatelyEqual(float expected, float actual, string message) => UnityEngine.Assertions.Assert.AreApproximatelyEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(ushort expected, ushort actual) => UnityEngine.Assertions.Assert.AreEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(ushort expected, ushort actual, string message) => UnityEngine.Assertions.Assert.AreEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(byte expected, byte actual, string message) => UnityEngine.Assertions.Assert.AreEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(uint expected, uint actual, string message) => UnityEngine.Assertions.Assert.AreEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(char expected, char actual) => UnityEngine.Assertions.Assert.AreEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(char expected, char actual, string message) => UnityEngine.Assertions.Assert.AreEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(sbyte expected, sbyte actual, string message) => UnityEngine.Assertions.Assert.AreEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(sbyte expected, sbyte actual) => UnityEngine.Assertions.Assert.AreEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(int expected, int actual) => UnityEngine.Assertions.Assert.AreEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(int expected, int actual, string message) => UnityEngine.Assertions.Assert.AreEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(uint expected, uint actual) => UnityEngine.Assertions.Assert.AreEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(byte expected, byte actual) => UnityEngine.Assertions.Assert.AreEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(short expected, short actual, string message) => UnityEngine.Assertions.Assert.AreEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(short expected, short actual) => UnityEngine.Assertions.Assert.AreEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual<T>(T expected, T actual, string message, IEqualityComparer<T> comparer) => UnityEngine.Assertions.Assert.AreEqual(expected, actual, message, comparer);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual<T>(T expected, T actual, string message) => UnityEngine.Assertions.Assert.AreEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual<T>(T expected, T actual) => UnityEngine.Assertions.Assert.AreEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(ulong expected, ulong actual, string message) => UnityEngine.Assertions.Assert.AreEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(ulong expected, ulong actual) => UnityEngine.Assertions.Assert.AreEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(UnityEngine.Object expected, UnityEngine.Object actual, string message) => UnityEngine.Assertions.Assert.AreEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(long expected, long actual) => UnityEngine.Assertions.Assert.AreEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreEqual(long expected, long actual, string message) => UnityEngine.Assertions.Assert.AreEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotApproximatelyEqual(float expected, float actual, string message) => UnityEngine.Assertions.Assert.AreNotApproximatelyEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotApproximatelyEqual(float expected, float actual, float tolerance, string message) => UnityEngine.Assertions.Assert.AreNotApproximatelyEqual(expected, actual, tolerance, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotApproximatelyEqual(float expected, float actual, float tolerance) => UnityEngine.Assertions.Assert.AreNotApproximatelyEqual(expected, actual, tolerance);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotApproximatelyEqual(float expected, float actual) => UnityEngine.Assertions.Assert.AreNotApproximatelyEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(short expected, short actual) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(short expected, short actual, string message) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(long expected, long actual, string message) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(uint expected, uint actual, string message) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(ushort expected, ushort actual) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(ushort expected, ushort actual, string message) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(int expected, int actual) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(int expected, int actual, string message) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(long expected, long actual) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(uint expected, uint actual) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(sbyte expected, sbyte actual, string message) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(char expected, char actual) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual<T>(T expected, T actual) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual<T>(T expected, T actual, string message) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual<T>(T expected, T actual, string message, IEqualityComparer<T> comparer) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual, message, comparer);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(char expected, char actual, string message) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(UnityEngine.Object expected, UnityEngine.Object actual, string message) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(byte expected, byte actual, string message) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(byte expected, byte actual) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(ulong expected, ulong actual) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(sbyte expected, sbyte actual) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual);
        [Conditional("UNITY_ASSERTIONS")] public static void AreNotEqual(ulong expected, ulong actual, string message) => UnityEngine.Assertions.Assert.AreNotEqual(expected, actual, message);
        [Conditional("UNITY_ASSERTIONS")] public static void IsFalse(bool condition) => UnityEngine.Assertions.Assert.IsFalse(condition);
        [Conditional("UNITY_ASSERTIONS")] public static void IsNotNull<T>(T value) where T : class => UnityEngine.Assertions.Assert.IsNotNull(value);
        [Conditional("UNITY_ASSERTIONS")] public static void IsNotNull(UnityEngine.Object value, string message) => UnityEngine.Assertions.Assert.IsNotNull(value, message);
        [Conditional("UNITY_ASSERTIONS")] public static void IsNotNull<T>(T value, string message) where T : class => UnityEngine.Assertions.Assert.IsNotNull(value, message);
        [Conditional("UNITY_ASSERTIONS")] public static void IsNull<T>(T value, string message) where T : class => UnityEngine.Assertions.Assert.IsNull(value, message);
        [Conditional("UNITY_ASSERTIONS")] public static void IsNull(UnityEngine.Object value, string message) => UnityEngine.Assertions.Assert.IsNull(value, message);
        [Conditional("UNITY_ASSERTIONS")] public static void IsNull<T>(T value) where T : class => UnityEngine.Assertions.Assert.IsNull(value);
        [Conditional("UNITY_ASSERTIONS")] public static void IsTrue(bool condition, string message) => UnityEngine.Assertions.Assert.IsTrue(condition, message);
        [Conditional("UNITY_ASSERTIONS")] public static void IsTrue(bool condition) => UnityEngine.Assertions.Assert.IsTrue(condition);

    }
}