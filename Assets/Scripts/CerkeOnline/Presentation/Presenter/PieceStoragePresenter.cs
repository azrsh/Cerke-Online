using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Presentation.View;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    /// <summary>
    /// PieceViewも自己の座標を変更するので, 権限が分散しててキモイ.
    /// </summary>
    public class PieceStoragePresenter : MonoBehaviour
    {
        [SerializeField] PiecePresenter piecePresenter = default;
        [SerializeField] Vector3 offset = default;
        [SerializeField] Vector3 placeInterval = default;
        [SerializeField] int lineLimit = default;
        [SerializeField] Encampment encampment = Encampment.Front;

        IPlayer player;

        private void Start()
        {
            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(OnGameReset);
        }

        void OnGameReset(IGame game)
        {
            player = game.GetPlayer(encampment);
            player.OnPieceStrageCahnged.TakeUntilDestroy(this).Subscribe(OnPieceStrageChanged);
        }

        void OnPieceStrageChanged(Unit unit)
        {
            IEnumerable<IReadOnlyPiece> pieces = player.GetPieceList();
            int i = 0;
            foreach (var piece in pieces)
            {
                PieceView pieceView;
                piecePresenter.ViewDatabese.TryGetValue(piece, out pieceView);

                if (pieceView != null)
                {
                    pieceView.transform.position = GetNextPlace(i);
                    pieceView.transform.rotation = GetPieceQuaternion(piece);
                }

                i++;
            }
        }

        Vector3 GetNextPlace(int index)
        {
            int x = index % lineLimit;
            int y = index / lineLimit + 1;
            return offset + new Vector3(x * placeInterval.x, y * placeInterval.y, transform.position.z);
        }

        Quaternion GetPieceQuaternion(IReadOnlyPiece piece)
        {
            int pieceAttitude = GetPieceAttitude(piece);
            return Quaternion.AngleAxis(pieceAttitude, Vector3.forward);
        }

        //PieceViewに同じコードがあるので統合したい
        int GetPieceAttitude(IReadOnlyPiece piece)
        {
            var owner = piece.Owner;
            if (owner == null) return -90;

            switch (owner.Encampment)
            {
                case Encampment.Front:
                    return 0;
                case Encampment.Back:
                    return 180;
            }

            return -90;
        }
    }
}