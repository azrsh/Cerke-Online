using System;
using UnityEngine;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities.Official.Hands
{
    /// <summary>
    /// 皇の動きを監視し, 皇再来を検出する.
    /// 皇が取られた場合の動作は未定義.
    /// </summary>
    public class TamObserver
    {   
        bool isMoved = false;                   //現在のターン中に駒が動いたか
        bool isPrevoiusMoved = false;           //前回のターンに駒が動いたか
        bool isTamenMako = false;               //皇再来が成立したか
        Vector2Int previousTurnPosition;        //前回のターン終了時の皇の座標
        Vector2Int currentPosition;             //現在のtamの座標

        public TamObserver(IObservable<Unit> onTurnChanged, IObservable<Unit> onEveruValueChangedOnBoard, IReadOnlyPiece tam)
        {
            previousTurnPosition = currentPosition = tam.Position;
            onEveruValueChangedOnBoard.Select(_ => tam.Position).DistinctUntilChanged().Subscribe(OnTamMoved);
            onTurnChanged.Subscribe(OnTurnChanged);
        }
        
        void OnTamMoved(Vector2Int position)
        {
            isTamenMako = isPrevoiusMoved || (isMoved && previousTurnPosition == position);
            isMoved = true;
            currentPosition = position;
        }

        void OnTurnChanged(Unit unit)
        {
            isPrevoiusMoved = isMoved;
            isMoved = false;
            previousTurnPosition = currentPosition;
        }

        public bool IsTamenMako()
        {
            return isTamenMako;
        }
    }
}