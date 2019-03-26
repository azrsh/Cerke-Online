using System;
using UnityEngine;
using UniRx;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Data.DataStructure
{
    [CreateAssetMenu(menuName = "ScriptableObject/PreGameSettings")]
    public class PreGameSettings : ScriptableObject
    {
        public IObservable<Unit> OnStartButtonClicked => onStartButtonClicked;
        readonly Subject<Unit> onStartButtonClicked = new Subject<Unit>();

        public int rulesetId;
        public FirstOrSecond firstOrSecond;
        public bool isZeroDistanceMovementPermitted;

        public void OnStartButton()
        {
            onStartButtonClicked.OnNext(Unit.Default);
        }
    }
}