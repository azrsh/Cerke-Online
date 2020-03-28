using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Data.DataStructure;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using Azarashi.CerkeOnline.Presentation.View;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    public class PiecePresenter : MonoBehaviour
    {
        [SerializeField] PiecePrefabsObject pieceViewPrefabs = default;
        [SerializeField] IReadOnlyPieceUnityEvent onPieceClicked = default;

        readonly Dictionary<IReadOnlyPiece, PieceView> database = new Dictionary<IReadOnlyPiece, PieceView>();
        public IReadOnlyDictionary<IReadOnlyPiece, PieceView> ViewDatabese => database;

        Vector3[,] columnMap;

        void Start()
        {
            IMapProvider<Vector3> columnMapProvider = GetComponentInChildren<IMapProvider<Vector3>>();
            columnMap = columnMapProvider.GetMap();

            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(Bind);
        }

        void Bind(IGame game)
        {
           game.SeasonSequencer.OnStart.TakeUntilDestroy(this).Select(_ => game).Subscribe(OnSeasonStart);
           OnSeasonStart(game);     //不格好
        }

        void OnSeasonStart(IGame game)
        {
            ClearPieceView();

            IntegerVector2 position = new IntegerVector2(0, 0);
            for (position.x = 0; position.x < Terminologies.LengthOfOneSideOfBoard; position.x++)
                for (position.y = 0; position.y < Terminologies.LengthOfOneSideOfBoard; position.y++)
                    InitializePieceView(position, columnMap, game);
        }

        void ClearPieceView()
        {
            foreach (var item in database.Select(pair => pair.Value.gameObject))
                Destroy(item);
            
            database.Clear();
        }

        void InitializePieceView(IntegerVector2 position, Vector3[,] columnMap, IGame game)
        {
            IBoard board = game.Board;
            IReadOnlyPiece piece = board.GetPiece(position);
            if (piece == null) return;

            Terminologies.PieceName pieceName;
            System.Enum.TryParse(piece.GetType().Name, out pieceName);
            GameObject pieceViewObject = Instantiate(pieceViewPrefabs.Prefabs[(int)pieceName]);
            pieceViewObject.transform.position = new Vector3(0, 0, -10);    //描画優先度の設定

            PieceView pieceViewComponent = pieceViewObject.GetComponent<PieceView>();
            pieceViewComponent.Initialize(piece, columnMap);
            pieceViewComponent.OnClicked.TakeUntilDestroy(this).Subscribe(onPieceClicked.Invoke);
            database.Add(piece, pieceViewComponent);
        }
    }

    [System.Serializable] public class IReadOnlyPieceUnityEvent : UnityEngine.Events.UnityEvent<IReadOnlyPiece> { }
    [System.Serializable] public class IntVector2UnityEvent : UnityEngine.Events.UnityEvent<IntegerVector2> { }
}