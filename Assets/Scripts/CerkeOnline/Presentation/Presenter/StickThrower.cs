using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Async;
using Azarashi.CerkeOnline.Domain.Entities;
using Random = UnityEngine.Random;

namespace Azarashi.CerkeOnline.Presentation.Presenter
{
    public class StickThrower : MonoBehaviour, IValueInputProvider<int>
    {
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

        public async UniTask<int> RequestValue()
        {
            throwStickButton.gameObject.SetActive(true);
            
            await throwStickButton.OnClickAsObservable().First().TakeUntilDestroy(this);

            throwStickButton.gameObject.SetActive(false);
            int number = Random.Range(0, 2) + Random.Range(0, 2) + Random.Range(0, 2) + Random.Range(0, 2) + Random.Range(0, 2);
            Debug.Log("投げ棒の数 : " + number);
            return number;
        }
    }
}