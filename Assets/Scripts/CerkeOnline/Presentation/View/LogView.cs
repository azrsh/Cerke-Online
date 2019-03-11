using UnityEngine;
using UnityEngine.UI;

namespace Azarashi.CerkeOnline.Presentation.View
{
    public class LogView : MonoBehaviour
    {
        [SerializeField] Text logField = default;

        void Awake()
        {
            UnityEngine.Application.logMessageReceived += OnSendLogMessage;
        }

        void OnSendLogMessage(string message, string stackTrace, LogType logType)
        {
            if (string.IsNullOrEmpty(message))
                return;

            logField.text += message + System.Environment.NewLine;
        }
    }
}