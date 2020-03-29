using System;
using UniRx;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands
{
    /// <summary>
    /// 皇の動きを監視し, 皇再来を検出する.
    /// 皇が取られた場合の動作は未定義.
    /// </summary>
    public class TamObserver
    {
        int numberOfTamenMako = 0;              //皇再来の成立回数
        bool isMoved = false;                   //現在のターン中に駒が動いたか
        bool isPrevoiusMoved = false;           //前回のターンに駒が動いたか
        PublicDataType.IntegerVector2 previousTurnPosition;        //前回のターン終了時の皇の座標
        PublicDataType.IntegerVector2 currentPosition;             //現在のtamの座標

        public TamObserver(IObservable<Unit> onTurnChanged, IObservable<Unit> onEveruValueChangedOnBoard, IReadOnlyPiece tam)
        {
            previousTurnPosition = currentPosition = tam.Position;
            onEveruValueChangedOnBoard.Select(_ => tam.Position).DistinctUntilChanged().Subscribe(OnTamMoved);
            onTurnChanged.Subscribe(OnTurnChanged);
        }
        
        void OnTamMoved(PublicDataType.IntegerVector2 position)
        {
            if (isPrevoiusMoved || (isMoved && previousTurnPosition == position)) numberOfTamenMako++;
            isMoved = true;
            currentPosition = position;
        }

        void OnTurnChanged(Unit unit)
        {
            isPrevoiusMoved = isMoved;
            isMoved = false;
            previousTurnPosition = currentPosition;
        }

        public int GetNumberOfTamenMako()
        {
            return numberOfTamenMako;
        }
    }
}