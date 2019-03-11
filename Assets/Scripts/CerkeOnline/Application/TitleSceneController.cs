using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Azarashi.Utilities;

namespace Azarashi.CerkeOnline.Application
{
    public class TitleSceneController : MonoBehaviour
    {
        [SerializeField] GameControllerInitilizeObject initilizeObject = default;
        [SerializeField] Button startOnlineButton = default;
        [SerializeField] Button startOfflineButton = default;

        SceneAsyncLoader sceneLoader;

        void Start()
        {
            sceneLoader = new SceneAsyncLoader("Main");

            startOnlineButton.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(OnStartOnlineButoon);
            startOfflineButton.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(OnStartOfflineButoon);

            startOnlineButton.interactable = false;
        }

        void OnStartOnlineButoon(Unit unit)
        {
            initilizeObject.isOnline = true;
            sceneLoader.ChangeScene();
        }

        void OnStartOfflineButoon(Unit unit)
        {
            initilizeObject.isOnline = false;
            sceneLoader.ChangeScene();
        }
    }
}