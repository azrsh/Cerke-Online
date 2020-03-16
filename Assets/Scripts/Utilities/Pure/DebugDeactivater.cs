using UnityEngine;

namespace Azarashi.Utilities
{
    public class DebugDeactivater : MonoBehaviour
    {
#if UNITY_EDITOR
        void Start()
        {
            Debug.unityLogger.logEnabled = false;
            //Screen.SetResolution()
        }
#endif
    }
}