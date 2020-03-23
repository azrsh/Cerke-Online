using System;
using UnityEngine;
using UniRx;
using Azarashi.Utilities;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Networking.Client;
using Azarashi.CerkeOnline.Networking.Components;

namespace Azarashi.CerkeOnline.Application
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public IObservable<IGame> OnGameReset => onGameReset;
        readonly Subject<IGame> onGameReset = new Subject<IGame>();

        [SerializeField] GameControllerInitilizeObject initilizeObject = default;
        [SerializeField] PreGameSettings preGameSettings = default;

        public IGame Game { get; private set; } = null;

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

        public ILogger SystemLogger { get; } = new Logger(new SystemLogHandler());

        public IReadOnlyServiceLocator ServiceLocator { get; } = new DefaultServiceLocator();

        public IPlayer LocalPlayer { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        RulesetList rulesetList;

        private void Start()
        {
            rulesetList = new RulesetList(ServiceLocator);
            preGameSettings.OnStartButtonClicked.TakeUntilDestroy(this).Subscribe(_ => NewGame());
        }

        void NewGame()
        {
            var ruleset = rulesetList.GetRuleset((int)preGameSettings.rulesetName);
            var localPlayerFirstOrSecond = GetFirstOrSecond(preGameSettings.firstOrSecond);
            var localPlayerEncampment = GetEncampment(preGameSettings.encampment);
            var remotePlayerEncampment = Terminologies.GetReversal(localPlayerEncampment);

            var firstPlayerEncampment = localPlayerFirstOrSecond == Terminologies.FirstOrSecond.First ? localPlayerEncampment : remotePlayerEncampment;

            Game = ruleset.CreateGameInstance(firstPlayerEncampment);
            LocalPlayer = Game.GetPlayer(localPlayerFirstOrSecond);
            onGameReset.OnNext(Game);
        }

        Terminologies.FirstOrSecond GetFirstOrSecond(Terminologies.FirstOrSecond firstOrSecond)
        {
            switch (firstOrSecond)
            {
            case Terminologies.FirstOrSecond.First:
            case Terminologies.FirstOrSecond.Second:
                return firstOrSecond;
            default:
                return (Terminologies.FirstOrSecond)UnityEngine.Random.Range(0, 1);
            }
        }

        Terminologies.Encampment GetEncampment(Terminologies.Encampment encampment)
        {
            switch (encampment)
            {
            case Terminologies.Encampment.Front:
            case Terminologies.Encampment.Back:
                return encampment;
            default:
                return (Terminologies.Encampment)UnityEngine.Random.Range(0, 1);
            }
        }
    }
}