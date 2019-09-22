using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    /// <summary>
    /// TransformMapとVector3Mapの両方を提供. 分離したい.
    /// </summary>
    public class ColumnMapProvider : MonoBehaviour, IMapProvider<Vector3>, IMapProvider<Transform>
    {
        Vector3[,] vector3Map = null;
        Transform[,] transformMap = null;

        public Vector3[,] GetMap()
        {
            if (vector3Map != null) return vector3Map;

            vector3Map = new Vector3[Terminologies.LengthOfOneSideOfBoard, Terminologies.LengthOfOneSideOfBoard];
            BoxCollider2D[] boxColliders = GetComponentsInChildren<BoxCollider2D>();
            for (int y = 0; y < vector3Map.GetLength(1); y++)
                for (int x = 0; x < vector3Map.GetLength(0); x++)
                    vector3Map[x, y] = boxColliders[y * vector3Map.GetLength(0) + x].transform.position;
            return vector3Map;
        }

        Transform[,] IMapProvider<Transform>.GetMap()
        {
            if (transformMap != null) return transformMap;

            transformMap = new Transform[Terminologies.LengthOfOneSideOfBoard, Terminologies.LengthOfOneSideOfBoard];
            BoxCollider2D[] boxColliders = GetComponentsInChildren<BoxCollider2D>();
            for (int y = 0; y < transformMap.GetLength(1); y++)
                for (int x = 0; x < transformMap.GetLength(0); x++)
                    transformMap[x, y] = boxColliders[y * transformMap.GetLength(0) + x].transform;
            
            return transformMap;
        }
    }
}