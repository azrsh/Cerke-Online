
namespace Azarashi.CerkeOnline.Domain.Entities
{
    public class Terminologies
    {
        public const int LengthOfOneSideOfBoard = 9;

        public enum BoardRow
        {
            K = 0, L = 1, N = 2, T = 3, Z = 4, X = 5, C = 6, M = 7, P = 8
        }

        public enum BoardLine
        {
            A = 0, E = 1, I = 2, U = 3, O = 4, Y = 5, AI = 6, AU = 7, IA = 8
        }

        public enum FirstOrSecond
        {
            First, Second
        }

        public enum FieldEffect
        {
            Normal, Tammua, Tarfe, Tanzo
        }

        public enum PieceName
        {
            None         = -1,

            Felkana      = 0,
            Elmer        = 1,
            Gustuer      = 2,
            Vadyrd       = 3,
            Stistyst     = 4,
            Dodor        = 5,
            Kua          = 6,
            Terlsk       = 7,
            Varxle       = 8,
            Ales         = 9,
            Tam          = 10
        }

        public const string Felkana      = "船";
        public const string Elmer        = "兵";
        public const string Gustuer      = "弓";
        public const string Vadyrd       = "車";
        public const string Stistyst     = "虎";
        public const string Dodor        = "馬";
        public const string Kua          = "筆";
        public const string Terlsk       = "巫";
        public const string Varxle       = "将";
        public const string Ales         = "王";
        public const string Tam          = "皇";
    }
}