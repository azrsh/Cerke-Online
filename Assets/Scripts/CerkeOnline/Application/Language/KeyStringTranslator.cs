
namespace Azarashi.CerkeOnline.Application.Language
{
    internal static class KeyStringTranslator
    {
        public static string Translate(string keyString, ILanguageTranslator translator)
        {
            TranslatableKeys key;
#if UNITY_EDITOR
            key = (TranslatableKeys)System.Enum.Parse(typeof(TranslatableKeys), keyString);
#else
             if (!System.Enum.TryParse(keyString, out key))
                return string.Empty;
#endif
            return translator.Translate(key);
        }
    }
}