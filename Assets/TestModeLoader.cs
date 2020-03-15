using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Azarashi.CerkeOnline.Data.DataStructure;
using Azarashi.CerkeOnline.Presentation;

public class TestModeLoader : MonoBehaviour
{
    [SerializeField] GameObject cameraPrefab = default;
    [SerializeField] GameObject eventSystemPrefab = default;
    [SerializeField] GameObject gameControllerPrefab = default;
    [SerializeField] PreGameSettings preGameSettings = default;

#if UNITY_EDITOR
    void Awake()
    {
        if (SceneManager.sceneCount > 1)
        {
            Destroy(gameObject);
            return;
        }

        GameObject.Instantiate(cameraPrefab);
        GameObject.Instantiate(eventSystemPrefab);

        var gameController = GameObject.Instantiate(gameControllerPrefab);
        gameController.GetComponent<PreGameSettingsSceneLoader>().enabled = false;
    }

    IEnumerator Start()
    {   
        yield return null;

        preGameSettings.rulesetName = Azarashi.CerkeOnline.Domain.Entities.Terminologies.RulesetName.celterno;
        preGameSettings.OnStartButton();
    }
#endif
}
