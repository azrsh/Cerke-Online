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
        bool IsInTarfe(PublicDataType.IntVector2 position);

        /// <summary>
        /// 指定されたマスが皇水か調べる.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        bool IsInTammua(PublicDataType.IntVector2 position);
    }
}