using System;
using UnityEngine;
using UniRx;
using Azarashi.Utilities;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.NoRule;
using Azarashi.CerkeOnline.Networking.Client;
using Azarashi.CerkeOnline.Networking.Components;
using Azarashi.CerkeOnline.Data.DataStructure;

namespace Azarashi.CerkeOnline.Application
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public IObservable<IGame> OnGameReset => onGameReset;
        readonly Subject<IGame> onGameReset = new Subject<IGame>();

        [SerializeField] GameControllerInitilizeObject initilizeObject = default;
        [SerializeField] PreGameSettings preGameSettings = default;

        public IGame Game => game;
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

        public ILogger SystemLogger => systemLogger;
        readonly ILogger systemLogger = new Logger(new SystemLogHandler());

        public IReadOnlyServiceLocator ServiceLocator => serviceLocator;
        readonly IServiceLocator serviceLocator = new DefaultServiceLocator();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            preGameSettings.OnStartButtonClicked.TakeUntilDestroy(this).Subscribe(_ => NewGame());
        }

        void NewGame()
        {
            game = new NoRuleGame();
            onGameReset.OnNext(game);
        }
    }
}