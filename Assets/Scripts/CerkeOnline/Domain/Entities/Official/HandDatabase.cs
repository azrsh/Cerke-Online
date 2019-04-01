using System;
using System.Linq;
using System.Collections.Generic;
using UniRx;
using Azarashi.Utilities.Collections;
using Azarashi.CerkeOnline.Domain.Entities.Official.Hands;
using Azarashi.CerkeOnline.Domain.Entities.Official.Hands.PieceStackProviders;

namespace Azarashi.CerkeOnline.Domain.Entities.Official
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
    public class HandDatabase : IHandDatabase
    {
        readonly IHand[] hands;

        public HandDatabase(IBoard board, IObservable<Unit> onTurnChanged)
        {
            const int NumberOfPieceStacksProviders = 10;
            IPieceStacksProvider[] pieceStacksProviders = new IPieceStacksProvider[NumberOfPieceStacksProviders]
                        { new LaAls(), new LaNyAnknish(), new LaMeunerfergal(), new LaNienulerless(), new LaNermetixaler(), new LaPysess(),
                          new LaPhertarsadElmss(), new LaVefisait(), new LaElmss(), new LaCeldinerss()};
            int[] baseScores = new int[NumberOfPieceStacksProviders]
                        {          50,                10,                    7,                    5,                    3,              3,
                                                3,                5,             3,                  3};
            int bounus = 2;

            this.hands = new IHand[NumberOfPieceStacksProviders * 2 + 2];
            for (int i = 0; i < NumberOfPieceStacksProviders; i++)
            {
                hands[i * 2] = new DefaultHand(pieceStacksProviders[i], baseScores[i]);
                hands[i * 2 + 1] = new LaDejixeceHand(pieceStacksProviders[i], bounus);
            }

            var tam = board.SearchPiece(Terminologies.PieceName.Tam);
            var tamObserver = new TamObserver(onTurnChanged, board.OnEveruValueChanged, tam);
            hands[NumberOfPieceStacksProviders * 2] = new LaTamadSemorkovo(-5);
            hands[NumberOfPieceStacksProviders * 2 + 1] = new TamenMako(-3, tamObserver);
        }

        public IHand[] SearchHands(IReadOnlyList<IReadOnlyPiece> pieces)
        {   
            return hands.Where(hand => hand != null && hand.GetNumberOfSuccesses(pieces) > 0).ToArray();
        }
    }
}