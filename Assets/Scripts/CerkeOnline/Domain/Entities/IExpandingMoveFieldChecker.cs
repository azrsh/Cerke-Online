using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IExpandingMoveFieldChecker
    {
        bool IsExpandedMoveField(PublicDataType.IntegerVector2 position);
    }
}