using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Azarashi.CerkeOnline.Domain.Entities;
using Random = UnityEngine.Random;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    public class StickThrower : MonoBehaviour, IValueInputProvider<int>
    {
        public bool IsRequestCompleted { get; private set; } = true;

        [SerializeField] Button throwStickButton = default;

        void Awake()
        {
            if(throwStickButton == null)
                throw new NullReferenceException();
        }

        private void Start()
        {
            throwStickButton.gameObject.SetActive(false);
        }

        public void RequestValue(Action<int> callback)
        {
            IsRequestCompleted = false;

            Action<Unit> action = _ =>
            {
                throwStickButton.gameObject.SetActive(false);
                int number = Random.Range(1, 5);
                Debug.Log("投げ棒の数 : " + number);
                callback(number);
                IsRequestCompleted = true;
            };

            throwStickButton.gameObject.SetActive(true);
            throwStickButton.OnClickAsObservable().First().TakeUntilDestroy(this).Subscribe(action);
        }
    }
}