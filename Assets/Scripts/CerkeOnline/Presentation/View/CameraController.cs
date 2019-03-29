using UnityEngine;
using UniRx;
using Azarashi.CerkeOnline.Application;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Presentation.View
{
    public class CameraController : MonoBehaviour
    {
        void Start()
        {
            GameController.Instance.OnGameReset.TakeUntilDestroy(this).Subscribe(OnGameReset);
        }

        void OnGameReset(IGame game)
        {
            switch(GameController.Instance.LocalPlayer.Encampment)
            {
            case Terminologies.Encampment.Front:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                return;
            case Terminologies.Encampment.Back:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                return;
            }
        }
    }
}