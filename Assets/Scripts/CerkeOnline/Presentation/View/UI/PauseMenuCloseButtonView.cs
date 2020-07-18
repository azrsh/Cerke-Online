using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UniRx;
using Azarashi.CerkeOnline.Application;

public class PauseMenuCloseButtonView : MonoBehaviour
{
    [SerializeField] Button closeButton = default;

    void Start()
    {
        Assert.IsNotNull(closeButton);

        closeButton.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(_ => SceneManager.UnloadSceneAsync(SceneName.MainSceneUI.PauseMenu));
    }
}
