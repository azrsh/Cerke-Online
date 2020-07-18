using TMPro;

namespace Azarashi.CerkeOnline.Application.Language
{
    public class TextData
    {
        public string Text { get; }
        public TMP_FontAsset FontAsset { get; }

        public TextData(string text, TMP_FontAsset fontAsset)
        {
            Text = text;
            FontAsset = fontAsset;
        }
    }
}