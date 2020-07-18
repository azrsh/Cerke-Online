using TMPro;

namespace Azarashi.CerkeOnline.Application.Language
{
    public struct LanguageData
    {
        public string Code { get; }
        public string Name { get; }
        public ILanguageTranslator Translator { get; }

        public LanguageData(string code, ILanguageTranslator translator)
        {
            Code = code;
            Translator = translator;

            Name = translator.Translate(TranslatableKeys.LanguageName).Text;
        }
    }
}