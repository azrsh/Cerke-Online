using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Data.DataStructure;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;

namespace Azarashi.CerkeOnline.Presentation.View
{
    [RequireComponent(typeof(Collider2D))]
    public class PieceView : MonoBehaviour, IPointerClickHandler
    {
        static readonly Vector3 PieceStrageScale = new Vector3(0.5f, 0.5f, 1f);
        static readonly Vector3 OnBoardScale = new Vector3(1.0f, 1.0f, 1.0f);

        [SerializeField] PieceMaterialsObject materials = default;
        IReadOnlyPiece piece;
        IBoard board;
        Vector3[,] columnMap = default;
        
        Collider2D pieceCollider;

        readonly Subject<IReadOnlyPiece> onClicked = new Subject<IReadOnlyPiece>();
        public IObservable<IReadOnlyPiece> OnClicked => onClicked;

        void Start()
        {
            if (materials == default)
                throw new NullReferenceException();

            board = GameController.Instance.Game.Board;
            //IObservable<IntVector2> observable = board.OnEveruValueChanged.TakeUntilDestroy(this).Select(_ => piece.Position).DistinctUntilChanged();
            //observable.Where(position => position != new IntVector2(-1, -1)).Subscribe(UpdateOnBoard);
            //observable.Where(position => position == new IntVector2(-1, -1)).Subscribe(UpdateOutOfBoard);
            board.OnEveruValueChanged.TakeUntilDestroy(this).Subscribe(UpdateView);

            pieceCollider = GetComponent<Collider2D>();
            pieceCollider.enabled = false;
        }

        public void Initialize(IReadOnlyPiece piece, Vector3[,] columnMap)
        {
            this.piece = piece;
            this.columnMap = columnMap;

            //TODO 駒の色系統の処理がいろいろクソなので直す
            GetComponentInChildren<SpriteRenderer>().material = ConvertPieceColorToMaterial(piece.Color);

            UpdateView(Unit.Default);
        }

        void UpdateView(Unit unit)
        {   
            IntegerVector2 position = piece.Position;
            if (position == new IntegerVector2(-1, -1))
            {
                UpdateOutOfBoard(position);
                return;
            }

            UpdateOnBoard(position);
        }

        void UpdateOnBoard(IntegerVector2 position)
        {
            //TODO マルチ対応
            float positionZ = transform.position.z;
            Vector3 columnPosition = columnMap[position.x, position.y];
            transform.position = new Vector3(columnPosition.x, columnPosition.y, positionZ);

            int pieceAttitude = GetPieceAttitude(piece);
            Quaternion quaternion = Quaternion.AngleAxis(pieceAttitude, Vector3.forward);
            transform.rotation = quaternion;

            transform.localScale = OnBoardScale;

            if (pieceCollider != null) pieceCollider.enabled = false;
        }

        void UpdateOutOfBoard(IntegerVector2 position)
        {
            transform.localScale = PieceStrageScale;
            if (pieceCollider != null) pieceCollider.enabled = true;
        }

        int GetPieceAttitude(IReadOnlyPiece piece)
        {
            var owner = piece.Owner;
            if (owner == null) return -90;

            switch (owner.Encampment)
            {
            case Terminologies.Encampment.Front:
                return 0;
            case Terminologies.Encampment.Back:
                return 180;
            }

            return -90;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClicked.OnNext(piece);
        }

        Material ConvertPieceColorToMaterial(Terminologies.PieceColor color)
        {
            switch (color)
            {
                case Terminologies.PieceColor.Black:
                    return materials.BlackMaterial;
                case Terminologies.PieceColor.Red:
                    return materials.RedMaterial;
                default:
                    return null;
            }
        }
    }
}