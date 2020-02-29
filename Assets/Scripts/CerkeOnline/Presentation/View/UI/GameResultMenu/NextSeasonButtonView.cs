using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;
using Azarashi.CerkeOnline.Application;

namespace Azarashi.CerkeOnline.Presentation.View
{
    public class NextSeasonButtonView : MonoBehaviour
    {
        [SerializeField] Button button = default;

        void Start()
        {
            button.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(_ =>
            {
                //季の変更メソッド

                SceneManager.UnloadSceneAsync(SceneName.MainSceneUI.SeasonResultMenu);
            });
        }
    }
}