using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.UseCase;

namespace Azarashi.CerkeOnline.Presentation.View
{
    public class NextSeasonButtonView : MonoBehaviour
    {
        [SerializeField] Button button = default;

        SeasonUseCase SeasonUseCase { get { return new SeasonUseCase(GameController.Instance.Game); } }

        void Start()
        {
            button.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(_ =>
            {
                if (SeasonUseCase == null) return;
                SeasonUseCase.Next();
                SceneManager.UnloadSceneAsync(SceneName.MainSceneUI.SeasonResultMenu);
            });
        }
    }
}