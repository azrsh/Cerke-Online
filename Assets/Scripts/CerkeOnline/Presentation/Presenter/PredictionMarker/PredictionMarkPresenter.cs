using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using Azarashi.CerkeOnline.Presentation.Presenter.Columns;

//TODO
//機能ごとにばらす
//Predictor, PositionConverter,MarkerSetter
namespace Azarashi.CerkeOnline.Presentation.Presenter.PredictionMarker
{
    //[RequireComponent(typeof(IMapProvider<Transform>))]
    public class PredictionMarkPresenter : MonoBehaviour
    {
        [SerializeField] GameObject columnParentObject = default;
        [SerializeField] GameObject predictionMarkerPrefab = default;
        PredictionMarkerObjects markerObjects;
        ColumnPositionConverter columnPositionConverter;
        MovePredictor movePredictor;

        //--------ここで保持すべきではない--------
        IReadOnlyPiece movingPiece;
        //--------------------------------------

        void Start()
        {
            //GameController呼び出したくない
            GameController.Instance.OnGameReset.Subscribe(game => movePredictor = new MovePredictor(game.Board));

            var mapProvider = columnParentObject.GetComponent<IMapProvider<Transform>>();
            columnPositionConverter = new ColumnPositionConverter(mapProvider);

            markerObjects = new PredictionMarkerObjects(predictionMarkerPrefab);
        }

        public void OnPieceSelected(IReadOnlyPiece movingPiece)
            => CalculateAndDisplay(movingPiece?.Position ?? new IntegerVector2(-1, -1), this.movingPiece = movingPiece);
        
        public void OnViaPositionSelected(IntegerVector2 position) 
        {
            /*中継地点からの移動先ハイライトは未実装*/
            CalculateAndDisplay(position, movingPiece);
            movingPiece = null;
        }

        public void OnTargetPositionSelected(IntegerVector2 position) => markerObjects.HideAllMarker();

        void CalculateAndDisplay(IntegerVector2 position, IReadOnlyPiece movingPiece)
        {
            var logicPositions = movePredictor.PredictMoveableColumns(position, movingPiece);
            var worldPositions = ConvertLogicPositionToWorldPosition(logicPositions);
            UpdateMarker(worldPositions);
        }

        IEnumerable<Vector3> ConvertLogicPositionToWorldPosition(IEnumerable<IntegerVector2> logicPositions)
        {
            return logicPositions.Select(value => columnPositionConverter.Convert(value));
        }

        void UpdateMarker(IEnumerable<Vector3> columns)
        {
            markerObjects.HideAllMarker();
            foreach (var column in columns)
                markerObjects.ShowMarker(column);
        }
    }
}