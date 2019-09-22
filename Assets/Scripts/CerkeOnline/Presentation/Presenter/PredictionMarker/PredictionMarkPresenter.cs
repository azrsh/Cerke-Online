using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Presentation.Presenter.Columns;

//TODO
//機能ごとにばらす
//Predictor, PositionConverter,MarkerSetter
namespace Azarashi.CerkeOnline.Presentation.Presenter.PredictionMarker
{
    //[RequireComponent(typeof(IMapProvider<Transform>))]
    public class PredictionMarkPresenter : MonoBehaviour
    {
        [SerializeField] GameObject columnParentObject;
        [SerializeField] GameObject predictionMarkerPrefab;
        PredictionMarkerObjects markerObjects;
        ColumnPositionConverter columnPositionConverter;
        MovePredictor movePredictor;

        void Start()
        {
            //GameController呼び出したくない
            GameController.Instance.OnGameReset.Subscribe(game => movePredictor = new MovePredictor(game.Board));
            
            var mapProvider = columnParentObject.GetComponent<IMapProvider<Transform>>();
            columnPositionConverter = new ColumnPositionConverter(mapProvider);

            markerObjects = new PredictionMarkerObjects(predictionMarkerPrefab);
        }

        public void OnPieceSelected(IReadOnlyPiece movingPiece) => CalculateAndDisplay(movingPiece);

        void CalculateAndDisplay(IReadOnlyPiece movingPiece)
        {
            var logicPositions = movePredictor.PredictMoveableColumns(movingPiece);
            var worldPositions = ConvertLogicPositionToWorldPosition(logicPositions);
            UpdateMarker(worldPositions);
        }

        IReadOnlyList<Vector3> ConvertLogicPositionToWorldPosition(IReadOnlyList<Vector2Int> logicPositions)
        {
            return logicPositions.Select(value => columnPositionConverter.Convert(value)).ToList();
        }


        void UpdateMarker(IReadOnlyList<Vector3> columns)
        {
            markerObjects.HideAllMarker();
            for (int i = 0; i < columns.Count; i++)
                markerObjects.ShowMarker(columns[i]);
        }
    }
}