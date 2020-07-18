using UnityEngine;
using UniRx;
using TMPro;

namespace Azarashi.CerkeOnline.Application.Language
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProTranslator : MonoBehaviour
    {
        [SerializeField] TranslatableKeys key = default;
        TextMeshProUGUI textComponent;
        TextMeshProUGUI TextComponent 
        {
            get {
                if (textComponent == null)
                    textComponent = GetComponent<TextMeshProUGUI>();

                return textComponent;
            } 
        }
        
        void Start() 
        {
            textComponent = GetComponent<TextMeshProUGUI>();
            LanguageManager.Instance.OnLanguageChanged.TakeUntilDestroy(this).Subscribe(UpdateText);
            UpdateText(LanguageManager.Instance.Translator);
        }

        private void UpdateText(ILanguageTranslator translator)
        {
            TextComponent.text = translator.Translate(key).Text;
            TextComponent.font = translator.Translate(key).FontAsset;
        }

#if false
        private void OnValidate()
        {
            if (!LanguageManager.Instance.TranslatableKeys.Contains(key.ToString()))
            { 
                Debug.LogError("Not found key");
                return;
            }

            UpdateText(LanguageManager.Instance.Translator);
        }
#endif
    }
}