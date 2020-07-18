using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UniRx;
using TMPro;
using Azarashi.CerkeOnline.Application.Language;

namespace Azarashi.CerkeOnline.Presentation.View.UI
{
    public class LogView : MonoBehaviour
    {
        [SerializeField] TMP_Text logField = default;
        [SerializeField] ContentSizeFitter contentSizeFitter = default;
        [SerializeField] ScrollRect scrollRect = default;

        void Awake()
        {
            Assert.IsNotNull(logField);
            Assert.IsNotNull(contentSizeFitter);
            Assert.IsNotNull(scrollRect);

            UnityEngine.Application.logMessageReceived += OnSendLogMessage;
        }

        void Start()
        {
            LanguageManager.Instance.OnLanguageChanged.Subscribe(translator => 
                logField.font = translator.Translate(TranslatableKeys.LoseMessage).FontAsset
            );
        }

        void OnSendLogMessage(string message, string stackTrace, LogType logType)
        {
            if (!UnityEngine.Application.isPlaying)
                return;

            if (string.IsNullOrEmpty(message))
                return;

            logField.text += message + System.Environment.NewLine;
            contentSizeFitter.SetLayoutVertical();
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }
}