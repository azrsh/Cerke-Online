using UnityEngine;

namespace Azarashi.CerkeOnline.Application
{
    /// <summary>
    /// GameControllerの初期化情報をシーン間で共有するためのScriptableObject
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/GameControllerInitilizeObject")]
    public class GameControllerInitilizeObject : ScriptableObject
    {
        [HideInInspector] public bool isOnline = false;
        [HideInInspector] public string clientId;
    }
}