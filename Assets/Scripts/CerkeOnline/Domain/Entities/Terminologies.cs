
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

        /// <summary>
        /// 先攻後攻を区別する.
        /// </summary>
        public enum FirstOrSecond
        {
            First, Second
        }

        /// <summary>
        /// 陣地の手前と奥を区別する. 盤上座標のy成分が大きいほうを手前, 小さいほうを奥とする.
        /// </summary>
        public enum Encampment
        {
            Front, Back
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

        public enum RulesetName
        {
            NoRule = 0,     //ルール無し
            celterno = 1,   //官定
            tamajtel = 2,   //硬皇力
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

        public static FirstOrSecond GetReversal(FirstOrSecond firstOrSecond)
        {
            switch (firstOrSecond)
            {
            case FirstOrSecond.First:
                return FirstOrSecond.Second;
            case FirstOrSecond.Second:
                return FirstOrSecond.First;
            default:
                throw new System.ArgumentException("値が無効です.");
            }
        }

        public static Encampment GetReversal(Encampment encampment)
        {
            switch (encampment)
            {
            case Encampment.Front:
                return Encampment.Back;
            case Encampment.Back:
                return Encampment.Front;
            default:
                throw new System.ArgumentException("値が無効です.");
            }
        }
    }
}