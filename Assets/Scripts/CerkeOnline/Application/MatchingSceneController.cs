using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Mirror;
using Azarashi.CerkeOnline.Networking;

namespace Azarashi.CerkeOnline.Application
{
    [RequireComponent(typeof(MatchingSceneUIPresenter))]
    public class MatchingSceneController : MonoBehaviour
    {
        //[SerializeField] GameControllerInitilizeObject initilizeObject = default;
        //SceneAsyncLoader sceneLoader;

        MatchingSceneUIPresenter uiPresenter;

        void Start()
        {
            uiPresenter = GetComponent<MatchingSceneUIPresenter>();
            uiPresenter.SetStatus(ConnectionStatus.Nothing);
            //sceneLoader = new SceneAsyncLoader("Main");

            this.UpdateAsObservable().TakeUntilDestroy(this).Select(_ => GetConnectionStatus()).DistinctUntilChanged().Subscribe(uiPresenter.SetStatus);
        }

        ConnectionStatus GetConnectionStatus()
        {
            if (NetworkServer.active)
            {
                if (NetworkManager.singleton.IsClientConnected())
                    return ConnectionStatus.Host;

                return ConnectionStatus.Server;
            }

            if (NetworkManager.singleton.IsClientConnected())
                return ConnectionStatus.ConnectedRemoteClient;

            NetworkClient client = NetworkManager.singleton.client;
            if (client != null && client.connection != null && client.connection.connectionId != -1)
                return ConnectionStatus.ConnectiongRemoteClient;

            return ConnectionStatus.Nothing;
        }

    }
}