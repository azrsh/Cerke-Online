using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;
using Azarashi.CerkeOnline.Presentation.View.UI;
using Azarashi.CerkeOnline.Data.DataStructure;

//本当はusingしたくない
using UnityEngine.UI;

namespace Azarashi.CerkeOnline.Presentation.Presenter.UI
{
    public class PreGameSettingsPresenter : MonoBehaviour
    {
        [SerializeField] PreGameSettings preGameSettings = default;

        [SerializeField] GameRuleSelectionView gameRuleSelectionView = default;
        [SerializeField] FirstOrSecondSelectionView firstOrSecondSelectionView = default;

        //Viewコンポーネントをはさむべき？（描画のための特別な処理はないので迷う）
        [SerializeField] Toggle ZeroDistanceMovementPermissionToggle = default;
        [SerializeField] Button startButton = default;

        void Start()
        {
            Bind();
        }

        void Bind()
        {
            gameRuleSelectionView.OnDropDownChanged.TakeUntilDestroy(this).Subscribe(value => preGameSettings.rulesetId = value);
            firstOrSecondSelectionView.OnDropDownChanged.TakeUntilDestroy(this).Subscribe(value => preGameSettings.firstOrSecond = (FirstOrSecond)value);

            ZeroDistanceMovementPermissionToggle.OnValueChangedAsObservable().TakeUntilDestroy(this).Subscribe(value => preGameSettings.isZeroDistanceMovementPermitted = value);
            startButton.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(_ => preGameSettings.OnStartButton());
            preGameSettings.OnStartButtonClicked.TakeUntilDestroy(this).Subscribe(_ => Destroy(gameObject));
        }
    }
}