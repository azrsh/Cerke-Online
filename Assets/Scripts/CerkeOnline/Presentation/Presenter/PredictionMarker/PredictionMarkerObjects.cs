using System;
using UnityEngine;
using UniRx;
using UniRx.Toolkit;
using Azarashi.Utilities;

namespace Azarashi.CerkeOnline.Presentation.Presenter.PredictionMarker
{
    public class PredictionMarkerObjects
    {
        ObjectPool<Transform> predictionMarkerPool;
        Action markerReturnAction = null;

        public PredictionMarkerObjects(GameObject predictionMarkerPrefab)
        {
            predictionMarkerPool = new TransformObjectPool(predictionMarkerPrefab);
        }

        public void HideAllMarker()
        {
            markerReturnAction?.Invoke();
            markerReturnAction = null;
        }

        public Transform ShowMarker(Vector3 position)
        {
            var marker = predictionMarkerPool.Rent();
            position.z = -5;            //描画優先度の設定
            marker.position = position;
            markerReturnAction += () => predictionMarkerPool.Return(marker);
            return marker;
        }
    }
}