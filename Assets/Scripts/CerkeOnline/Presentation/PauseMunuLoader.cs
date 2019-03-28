using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Azarashi.Utilities;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Presentation.Presenter.Inputs;

namespace Azarashi.CerkeOnline.Presentation
{
    [RequireComponent(typeof(IInputEventProvider))]
    public class PauseMunuLoader : MonoBehaviour
    {   
        void Start()
        {
            var sceneLoader = new SceneAsyncLoader(SceneName.MainSceneUI.PauseMenu, LoadSceneMode.Additive);
            GetComponent<IInputEventProvider>().OnPauseButton.TakeUntilDestroy(this).Subscribe(_ => sceneLoader.ChangeScene());
        }
    }
}