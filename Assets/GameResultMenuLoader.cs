using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Azarashi.CerkeOnline.Application;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    public class GameResultMenuLoader : MonoBehaviour
    {
        void Start()
        {
            GameController.Instance.OnGameReset
                .TakeUntilDestroy(this).Subscribe(game =>
                {
                    game.OnGameEnd.TakeUntilDestroy(this).Subscribe(_ =>
                        SceneManager.LoadSceneAsync(SceneName.MainSceneUI.GameResultMenu, LoadSceneMode.Additive)
                    );
                });
        }
    }
}
