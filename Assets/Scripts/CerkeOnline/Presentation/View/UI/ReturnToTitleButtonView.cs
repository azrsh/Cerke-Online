using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UniRx;
using Azarashi.CerkeOnline.Application;

namespace Azarashi.CerkeOnline.Presentation.View
{
    public class ReturnToTitleButtonView : MonoBehaviour
    {
        [SerializeField] Button button = default;

        void Start()
        {
            Assert.IsNotNull(button);

            button.OnClickAsObservable().TakeUntilDestroy(this).Subscribe(_ =>
            {
                //CerkeOnline.Application.GameController.OnQuit();
                SceneManager.LoadScene(SceneName.Title);
            });
        }
    }
}