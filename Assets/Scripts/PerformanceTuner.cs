using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceTuner : MonoBehaviour
{
    [SerializeField] int targetFrameRate = 30;
    [SerializeField] bool isVSyncActive = true;

    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
        QualitySettings.vSyncCount = isVSyncActive ? 1 : 0;
    }
}
