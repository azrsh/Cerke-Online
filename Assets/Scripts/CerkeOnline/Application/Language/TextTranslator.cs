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
        
        void Start() 
        {
            textComponent = GetComponent<Text>();
            LanguageManager.Instance.OnLanguageChanged.TakeUntilDestroy(this).Subscribe(UpdateText);
        }

        private void UpdateText(ILanguageTranslator translator)
        {
            textComponent.text = translator.Translate(key.ToString());
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!LanguageManager.Instance.TranslatableKeys.Contains(key.ToString()))
            { 
                Debug.LogError("Not found key");
                return;
            }

            //UpdateText();
        }
#endif
    }
}