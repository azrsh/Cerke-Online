using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Azarashi.CerkeOnline.Presentation.View
{
    public class QuitButtonView : MonoBehaviour
    {
        [SerializeField] Button button = default;

        void Start()
        {
            button.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(_ =>
            {
                //CerkeOnline.Application.GameController.OnQuit();
                UnityEngine.Application.Quit();
            });
        }
    }
}