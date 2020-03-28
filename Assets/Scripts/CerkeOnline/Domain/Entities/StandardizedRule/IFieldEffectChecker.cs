using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule
{
    internal interface IFieldEffectChecker : IExpandingMoveFieldChecker
    {
        /// <summary>
        /// 指定されたマスが皇処かを調べる.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        bool IsInTarfe(Vector2Int position);

        /// <summary>
        /// 指定されたマスが皇水か調べる.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        bool IsInTammua(Vector2Int position);
    }
}