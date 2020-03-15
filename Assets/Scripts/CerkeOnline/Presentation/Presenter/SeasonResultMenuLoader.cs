using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Azarashi.CerkeOnline.Application;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    public class SeasonResultMenuLoader : MonoBehaviour
    {
        void Start()
        {
            GameController.Instance.OnGameReset
                .TakeUntilDestroy(this).Subscribe(game =>
                {
                    game.SeasonSequencer.OnEnd.TakeUntilDestroy(this).Subscribe(_ => 
                        SceneManager.LoadSceneAsync(SceneName.MainSceneUI.SeasonResultMenu, LoadSceneMode.Additive)
                    );
                });
        }
    }
}