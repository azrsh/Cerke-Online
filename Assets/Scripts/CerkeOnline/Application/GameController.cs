using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.NoneRule;
using Azarashi.CerkeOnline.Networking.Client;
using Azarashi.CerkeOnline.Networking.Components;

namespace Azarashi.CerkeOnline.Application
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        [SerializeField] GameControllerInitilizeObject initilizeObject = default;

        public IGame Game
        {
            get
            {
                if (game == null) NewGame();
                return game;
            }
        }
        IGame game = null;

        public IServerDelegate ServerDelegate
        {
            get
            {
                if (!initilizeObject.isOnline)
                    if (serverDelegate == null) serverDelegate = new MockServer();
                else
                    if (serverDelegate == null) serverDelegate = FindObjectOfType<ServerDelegateComponent>();
                
                return serverDelegate;
            }
        }
        IServerDelegate serverDelegate;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        void NewGame()
        {
            game = new NoneRuleGame();
        }
    }
}