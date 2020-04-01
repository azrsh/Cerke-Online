using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Azarashi.CerkeOnline.Application.Language
{
    [RequireComponent(typeof(Text))]
    public class TextTranslator : MonoBehaviour
    {
        [SerializeField] TranslatableKeys key = default;
        Text textComponent;
        Text TextComponent 
        {
            get {
                if (textComponent == null)
                    textComponent = GetComponent<Text>();

                return textComponent;
            } 
        }
        
        void Start() 
        {
            textComponent = GetComponent<Text>();
            LanguageManager.Instance.OnLanguageChanged.TakeUntilDestroy(this).Subscribe(UpdateText);
            UpdateText(LanguageManager.Instance.Translator);
        }

        private void UpdateText(ILanguageTranslator translator)
        {
            TextComponent.text = translator.Translate(key); 
        }

#if UNITY_EDITOR
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