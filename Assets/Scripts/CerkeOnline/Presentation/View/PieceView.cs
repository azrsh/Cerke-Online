using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Data.DataStructure;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Presentation.View
{
    [RequireComponent(typeof(Collider2D))]
    public class PieceView : MonoBehaviour, IPointerClickHandler
    {
        readonly Vector3 PieceStrageScale = new Vector3(0.5f, 0.5f, 1f);
        readonly Vector3 OnBoardScale = new Vector3(1.0f, 1.0f, 1.0f);

        [SerializeField] PieceMaterialsObject materials = default;
        IReadOnlyPiece piece;
        IBoard board;
        Vector3[,] columnMap = default;

        Collider2D pieceCollider;

        void Start()
        {
            if (materials == default)
                throw new NullReferenceException();

            board = GameController.Instance.Game.Board;
            board.OnEveruValueChanged.TakeUntilDestroy(this).Subscribe(UpdateView);

            pieceCollider = GetComponent<Collider2D>();
            pieceCollider.enabled = false;
        }

        public void Initialize(IReadOnlyPiece piece, Vector3[,] columnMap)
        {
            this.piece = piece;
            this.columnMap = columnMap;

            //TODO 駒の色系統の処理がいろいろクソなので直す
            GetComponentInChildren<SpriteRenderer>().material = piece.Color == 0 ? materials.BlackMaterial : materials.RedMaterial;

            UpdateView(Unit.Default);
        }

        void UpdateView(Unit unit)
        {   
            Vector2Int position = piece.Position;
            if (position == new Vector2Int(-1, -1))
            {
                transform.localScale = PieceStrageScale;
                if (GetComponent<Collider>() != null) GetComponent<Collider>().enabled = true;
                return;
            }
            
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

        int GetPieceAttitude(IReadOnlyPiece piece)
        {
            if (piece.Owner == GameController.Instance.Game.FirstPlayer)
                return 0;
            if(piece.Owner == GameController.Instance.Game.SecondPlayer)
                return 180;

            return 90;
        }

        public void OnPointerClick(PointerEventData eventData)
        {

        }
    }
}