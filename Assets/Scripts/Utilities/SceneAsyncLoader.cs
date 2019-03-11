using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Azarashi.Utilities
{
    public class SceneAsyncLoader
    {
        AsyncOperation asyncOperation;

        public SceneAsyncLoader(string sceneName)
        {
            if (!ContainsScene(sceneName))
            {
                Debug.LogError("That Scene doese not exist.");
                return;
            }

            asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            asyncOperation.allowSceneActivation = false;
        }

        public void ChangeScene()
        {
            asyncOperation.allowSceneActivation = true;
        }

        //分離すべき
        bool ContainsScene(string sceneName)
        {
            List<string> sceneNameList = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(scene => scene.path).Select(path =>
            {
                int slash = path.LastIndexOf("/");
                int dot = path.LastIndexOf(".");
                return path.Substring(slash + 1, dot - slash - 1);
            }).ToList();

            return sceneNameList.Contains(sceneName);
        }
    }
}