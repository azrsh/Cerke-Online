using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IExpandingMoveFieldChecker
    {
        bool IsExpandedMoveField(PublicDataType.IntVector2 position);
    }
}