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

            for (int x = 0; x < Terminologies.LengthOfOneSideOfBoard; x++)
                for (int y = 0; y < Terminologies.LengthOfOneSideOfBoard; y++)
                    InitializePieceView(new IntegerVector2(x, y), columnMap, game);
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

            GameObject pieceViewObject = Instantiate(pieceViewPrefabs.Prefabs[(int)piece.Name]);
            pieceViewObject.transform.position = new Vector3(0, 0, -10);    //描画優先度の設定

            PieceView pieceViewComponent = pieceViewObject.GetComponent<PieceView>();
            pieceViewComponent.Initialize(piece, columnMap);
            pieceViewComponent.OnClicked.TakeUntilDestroy(this).Subscribe(onPieceClicked.Invoke);
            database.Add(piece, pieceViewComponent);
        }
    }

    [System.Serializable] public class IReadOnlyPieceUnityEvent : UnityEngine.Events.UnityEvent<IReadOnlyPiece> { }
}