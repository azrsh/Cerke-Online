using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Application.Language
{
    public static class PieceNameTranslator
    {
        public static string Translate(Terminologies.PieceName pieceName, ILanguageTranslator translator)
        {
            var keyString = "Pieces" + pieceName.ToString();
            return KeyStringTranslator.Translate(keyString, translator);
        }
    }
}