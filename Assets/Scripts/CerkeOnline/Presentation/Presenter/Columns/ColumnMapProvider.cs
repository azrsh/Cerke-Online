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
            for (int i = 0; i < vector3Map.GetLength(1); i++)
                for (int j = 0; j < vector3Map.GetLength(0); j++)
                    vector3Map[i, j] = boxColliders[j * vector3Map.GetLength(0) + i].transform.position;
            return vector3Map;
        }

        Transform[,] IMapProvider<Transform>.GetMap()
        {
            if (transformMap != null) return transformMap;

            transformMap = new Transform[Terminologies.LengthOfOneSideOfBoard, Terminologies.LengthOfOneSideOfBoard];
            BoxCollider2D[] boxColliders = GetComponentsInChildren<BoxCollider2D>();
            for (int i = 0; i < transformMap.GetLength(1); i++)
                for (int j = 0; j < transformMap.GetLength(0); j++)
                    transformMap[i, j] = boxColliders[j * transformMap.GetLength(0) + i].transform;
            return transformMap;
        }
    }
}