using System.Collections.Generic;
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
        [SerializeField] FirstOrSecond firstOrSecond = FirstOrSecond.First; //TODO LocalPlayerに変える

        IPlayer player;
        
        private void Start()
        {
            player = GameController.Instance.Game.GetPlayer(firstOrSecond);
            player.OnPieceStrageCahnged.TakeUntilDestroy(this).Subscribe(OnPieceStrageChanged);
        }

        void OnPieceStrageChanged(Unit unit)
        {
            IReadOnlyList<IReadOnlyPiece> pieces = player.GetPieceList();
            for(int i = 0;i < pieces.Count;i++)
            {
                PieceView pieceView;
                piecePresenter.ViewDatabese.TryGetValue(pieces[i], out pieceView);

                if (pieceView != null)
                {
                    pieceView.transform.position = GetNextPlace(i);
                    pieceView.transform.rotation = Quaternion.identity;
                }
            }
        }

        Vector3 GetNextPlace(int index)
        {
            int x = index % lineLimit;
            int y = index / lineLimit + 1;
            return offset + new Vector3(x * placeInterval.x, y * placeInterval.y, transform.position.z);
        }
    }
}