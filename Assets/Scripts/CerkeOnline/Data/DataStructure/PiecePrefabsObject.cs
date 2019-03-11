using System;
using System.Collections.Generic;
using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Data.DataStructure
{
    [CreateAssetMenu(menuName = "ScriptableObject/PiecePrefabsObject")]
    public class PiecePrefabsObject : ScriptableObject
    {
        [SerializeField, Tooltip("この項目の要素数は, 駒の種類の数と同一でなければなりません.")] GameObject[] prefabs = default;
        public IReadOnlyList<GameObject> Prefabs { get { return prefabs; } }

        void OnValidate()
        {
            if (prefabs.Length != Enum.GetNames(typeof(Terminologies.PieceName)).Length)
                Debug.LogWarning("要素数が不正です.");
        }
    }
}