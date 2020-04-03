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

            Vessel       = 0,
            Pawn         = 1,
            Archer       = 2,
            Chariot      = 3,
            Tiger        = 4,
            Horse        = 5,
            Officer      = 6,
            Shaman       = 7,
            General      = 8,
            King         = 9,
            Minds        = 10
        }

        public enum HandName
        {
            TheUnbeatable,
            TheSocialOrder,
            TheCulture,
            TheCavalry,
            TheAttack,
            TheKing,
            TheAnimals,
            TheArmy,
            TheComrades,
            TheDeadlyArmy,
            TheUnbeatableFlash,
            TheSocialOrderFlash,
            TheCultureFlash,
            TheCavalryFlash,
            TheAttackFlash,
            TheKingFlash,
            TheAnimalsFlash,
            TheArmyFlash,
            TheComradesFlash,
            TheDeadlyArmyFlash,
            TheStepping,
            TheFutileMove
        }

        public static HandName ToFlash(HandName handName)
        {
            switch (handName)
            {
                case HandName.TheUnbeatable:
                    return HandName.TheUnbeatableFlash;
                case HandName.TheSocialOrder:
                    return HandName.TheSocialOrderFlash;
                case HandName.TheCulture:
                    return HandName.TheCultureFlash;
                case HandName.TheCavalry:
                    return HandName.TheCavalryFlash;
                case HandName.TheAttack:
                    return HandName.TheAttackFlash;
                case HandName.TheKing:
                    return HandName.TheKingFlash;
                case HandName.TheAnimals:
                    return HandName.TheAnimalsFlash;
                case HandName.TheArmy:
                    return HandName.TheArmyFlash;
                case HandName.TheComrades:
                    return HandName.TheComradesFlash;
                case HandName.TheDeadlyArmy:
                    return HandName.TheDeadlyArmyFlash;
                default:
                    return handName;
            }
        }

        public enum RulesetName
        {
            StandardizedRule = 0,   //官定
            Tamajtel = 1,   //硬皇力
            NoRule = 2,     //ルール無し
        }

        public enum PieceColor
        {
            Black=278,Red=378
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

        public enum Season
        {
            None = 0, Spring = 1, Summer = 2, Autumn = 3, Winter = 4
        };

        public static Season GetNextSeason(Season season)
        {
            switch (season)
            {
                case Season.Spring:
                    return Season.Summer;
                case Season.Summer:
                    return Season.Autumn;
                case Season.Autumn:
                    return Season.Winter;
                case Season.Winter:
                default:
                    return default; //defalt = (Season)0
            }
        }
    }
}