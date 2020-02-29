using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Azarashi.CerkeOnline.Application;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    public class SeasonResultMenuLoader : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GameController.Instance.OnGameReset
                .TakeUntilDestroy(this).Subscribe(game =>
                {
                    game.OnSeasonEnd.TakeUntilDestroy(this).Subscribe(_ => 
                        SceneManager.LoadSceneAsync(SceneName.MainSceneUI.SeasonResultMenu, LoadSceneMode.Additive)
                    );
                });
        }
    }
}