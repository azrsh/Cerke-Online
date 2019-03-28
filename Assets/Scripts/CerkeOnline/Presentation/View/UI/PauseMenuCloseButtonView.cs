using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;
using Azarashi.CerkeOnline.Application;

public class PauseMenuCloseButtonView : MonoBehaviour
{
    [SerializeField] Button closeButton = default;

    void Start()
    {
        closeButton.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(_ => SceneManager.UnloadSceneAsync(SceneName.MainSceneUI.PauseMenu));
    }
}
