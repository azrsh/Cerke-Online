using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UniRx;

namespace Azarashi.CerkeOnline.Presentation.View
{
    public class QuitButtonView : MonoBehaviour
    {
        [SerializeField] Button button = default;

        void Start()
        {
            Assert.IsNotNull(button);

            button.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(_ =>
            {
                //CerkeOnline.Application.GameController.OnQuit();
                UnityEngine.Application.Quit();
            });
        }
    }
}