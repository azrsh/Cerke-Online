using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Mirror;
using Azarashi.CerkeOnline.Networking;

namespace Azarashi.CerkeOnline
{
    public class MatchingSceneUIPresenter : MonoBehaviour
    {
        [SerializeField] Text statusText = default;
        [SerializeField] Button runAsServerButton = default;
        [SerializeField] Button runAsHostButton = default;
        [SerializeField] InputField serverAddressInputField = default;
        [SerializeField] Button connectServerButton = default;

        void Start()
        {
            serverAddressInputField.text = NetworkManager.singleton.networkAddress;

            runAsServerButton.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(OnRunAsServerButton);
            runAsHostButton.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(OnRunAsHostButton);
            connectServerButton.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(OnConnectServerButton);
        }

        public void SetStatus(ConnectionStatus connectionStatus)
        {
            string status = string.Empty;
            switch (connectionStatus)
            {
            case ConnectionStatus.Server:
                status = "サーバーとして起動中…";
                break;
            case ConnectionStatus.Host:
                status = "ホストとして起動中…";
                break;
            case ConnectionStatus.ConnectedRemoteClient:
                status = "サーバーとの接続完了";
                break;
            case ConnectionStatus.ConnectiongRemoteClient:
                status = "サーバーと接続中…";
                break;
            case ConnectionStatus.Nothing:
            default:
                status = "接続なし";
                break;
            }

            statusText.text = status;
        }
        
        void OnRunAsServerButton(Unit unit)
        {
            NetworkManager.singleton.StartServer();
        }

        void OnRunAsHostButton(Unit unit)
        {
            NetworkManager.singleton.StartHost();
        }

        void OnConnectServerButton(Unit unit)
        {
            NetworkManager.singleton.networkAddress = serverAddressInputField.text;
            NetworkManager.singleton.StartClient();
        }
    }
}