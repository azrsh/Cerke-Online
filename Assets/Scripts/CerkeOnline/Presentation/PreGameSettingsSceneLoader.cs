using UnityEngine;
using UnityEngine.SceneManagement;
using Azarashi.CerkeOnline.Application;

namespace Azarashi.CerkeOnline.Presentation
{
    public class PreGameSettingsSceneLoader : MonoBehaviour
    {
        void Start()
        {
            SceneManager.LoadScene(SceneName.MainSceneUI.PreGameSettings, LoadSceneMode.Additive);
        }
    }
}
