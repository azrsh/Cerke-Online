using UnityEngine;
using UnityEngine.UI;

namespace Azarashi.CerkeOnline.Presentation.View.UI
{
    public class LogView : MonoBehaviour
    {
        [SerializeField] Text logField = default;
        [SerializeField] ContentSizeFitter contentSizeFitter = default;
        [SerializeField] ScrollRect scrollRect = default;

        void Awake()
        {
            UnityEngine.Application.logMessageReceived += OnSendLogMessage;
        }

        void OnSendLogMessage(string message, string stackTrace, LogType logType)
        {
            if (string.IsNullOrEmpty(message))
                return;

            logField.text += message + System.Environment.NewLine;
            contentSizeFitter.SetLayoutVertical();
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }
}