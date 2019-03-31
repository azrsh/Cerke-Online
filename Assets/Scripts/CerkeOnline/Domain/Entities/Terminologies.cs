using System.Collections.Generic;

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

        public static class Pieces
        {
            public const string Felkana = "船";
            public const string Elmer = "兵";
            public const string Gustuer = "弓";
            public const string Vadyrd = "車";
            public const string Stistyst = "虎";
            public const string Dodor = "馬";
            public const string Kua = "筆";
            public const string Terlsk = "巫";
            public const string Varxle = "将";
            public const string Ales = "王";
            public const string Tam = "皇";
        }

        public static class HandNameDictionary
        {
            /*
            理日辞書より引用

            《役名》　役はvoklisolと言う。
            -加点役　il vynut voklisol
            無抗行処(la als)、筆兵無傾(la ny anknish)、地心(la meunerfergal)、行行(la nienulerless)、王(la nermetixaler)、獣(la pysess)、
            闇戦之集(la phertarsa'd elmss)、馬弓兵(la vefisait)、戦集(la elmss)、助友(la celdinerss)

            -減点役
            撃皇(la tama'd semorkovo)、皇再来(tamen mako)

            -複合役
            同色(la dejixece)

            -官定ルールで採用されていない役
            声無行処(la ytartanerfergal) 
            */
            public static readonly IReadOnlyDictionary<string, string> PascalToLineparine = new Dictionary<string, string>()
            {
                {"LaAls", "la als"}, {"LaNyAnknish", "la ny anknish"}, {"LaMeunerfergal", "la meunerfergal"}, {"LaNienulerless", "la nienulerless" }, {"LaNermetixaler", "la nermetixaler"}, {"LaPysess", "la pysess"},
                {"LaPhertarsadElmss", "la phertarsa'd elmss" }, {"LaVefisait", "la vefisait"}, {"LaElmss", "la elmss"}, {"LaCeldinerss", "la celdinerss" },
                {"LaTamadSemorkovo", "la tama'd semorkovo" }, {"TamenMako", "tamen mako" },
                {"LaYtartanerfergal", "la ytartanerfergal" }
            };
            public static readonly IReadOnlyDictionary<string, string> PascalToJapanese = new Dictionary<string, string>()
            {
                {"LaAls", "無抗行処"}, {"LaNyAnknish", "筆兵無傾"}, {"LaMeunerfergal", "地心"}, {"LaNienulerless", "行行" }, {"LaNermetixaler", "王"}, {"LaPysess", "獣"},
                {"LaPhertarsadElmss", "闇戦之集" }, {"LaVefisait", "馬弓兵"}, {"LaElmss", "戦集"}, {"LaCeldinerss", "助友" },
                {"LaTamadSemorkovo", "撃皇" }, {"TamenMako", "皇再来" },
                {"LaYtartanerfergal", "声無行処" }
            };
        }

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