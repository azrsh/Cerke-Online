using System;
using static Azarashi.CerkeOnline.Domain.Entities.Terminologies;

namespace Azarashi.CerkeOnline.Domain.Entities
{
    public class PieceStack
    {
        public static bool IsNullOrEmpty(PieceStack pieceStack)
        {
            return pieceStack == null || pieceStack.IsEmpty();
        }



        public PieceName PieceName { get; private set; }
        public int StackCount { get; private set; }

        public PieceStack(PieceName pieceName, int stackCount)
        {
            PieceName = pieceName;
            StackCount = stackCount;
        }

        public bool IsEmpty()
        {
            return StackCount <= 0;
        }
        
        /// <summary>
        /// PieceStackに指定された数の駒を追加する.
        /// </summary>
        /// <param name="stackCount"></param>
        public void Push(int stackCount)
        {
            StackCount += stackCount;
        }

        /// <summary>
        /// PieceStackから指定された数の駒を取す.
        /// </summary>
        /// <param name="popCount"></param>
        /// <returns>実際に取り出せた数.</returns>
        public int Pop(int popCount)
        {
            popCount = Math.Min(StackCount, popCount);
            StackCount -= popCount;
            return popCount;
        }

        public PieceStack Split(int splitCount)
        {
            splitCount = Pop(splitCount);
            return new PieceStack(PieceName, splitCount);
        }
    }
}