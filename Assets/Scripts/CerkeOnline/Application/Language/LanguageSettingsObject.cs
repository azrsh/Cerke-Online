using UnityEngine;

namespace Azarashi.CerkeOnline.Application.Language
{
    [CreateAssetMenu(menuName = "ScriptableObject/LanguageSettingsObject")]
    public partial class LanguageSettingsObject : ScriptableObject
    {
        [SerializeField] string defaultLanguageCode = default;
        public string DefaultLanguageCode => defaultLanguageCode;
    }

    public partial class LanguageSettingsObject : ScriptableObject
    {
        public static LanguageSettingsObject Create()
        {
            return ScriptableObject.CreateInstance<LanguageSettingsObject>();
        }

        //拡張性を阻害している場合はObject.Instantiateを直接利用してください
        public static LanguageSettingsObject Instantiate(LanguageSettingsObject original)
        {
            return Object.Instantiate<LanguageSettingsObject>(original);
        }
    }
}