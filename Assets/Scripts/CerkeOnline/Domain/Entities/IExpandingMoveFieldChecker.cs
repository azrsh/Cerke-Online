using UnityEngine;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public interface IExpandingMoveFieldChecker
    {
        bool IsExpandedMoveField(Vector2Int position);
    }
}