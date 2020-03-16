using UnityEngine;

public class ScriptPauseer : MonoBehaviour
{
    [SerializeField] MonoBehaviour[] pauseScripts = default;

    public void SetActiveOfScripts(bool value)
    {
        for (int i = 0; i < pauseScripts.Length; i++)
        {
            pauseScripts[i].enabled = value;
        }
    }
}
