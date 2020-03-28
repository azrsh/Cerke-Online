using System.Collections.Generic;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction.DataStructure;

namespace Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction
{
    internal class MoveActionData
    {
        public IPiece MovingPiece { get; }
        public IPlayer Player { get; }
        public LinkedList<ColumnData> WorldPath { get; }
        public LinkedListNode<ColumnData> ViaPositionNode { get; }

        public MoveActionData(IPiece movingPiece, IPlayer player, LinkedList<ColumnData> worldPath, LinkedListNode<ColumnData> viaPositionNode)
        {
            MovingPiece = movingPiece;
            Player = player;
            WorldPath = worldPath;
            ViaPositionNode = viaPositionNode;
        }

        public PieceMovement GetCurrentPieceMovement(LinkedListNode<ColumnData> worldPathNode)
        {
            //for ViaPositionNode
            //return null; 
            return default;
        }
    }
}