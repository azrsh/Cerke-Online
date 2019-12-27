using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.Official.Pieces;

namespace Azarashi.CerkeOnline.Tests
{

    public class SozysozbotHandQuestionEngine
    {
        readonly Func<IReadOnlyPiece[], IHand[]> solveFunction;
        readonly int drillCount;
        
        public SozysozbotHandQuestionEngine(Func<IReadOnlyPiece[], IHand[]> solveFunction, int drillCount = 50)
        {
            this.solveFunction = solveFunction;
            this.drillCount = drillCount;
        }

        public void Solve()
        {
            var path = UnityEngine.Application.dataPath + "/Libraries/Selenium";
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            using (ChromeDriver driver = new ChromeDriver(path, options))
            {
                SozysozbotHandDrillPageDriver drillPageDriver = new SozysozbotHandDrillPageDriver(driver);
               
                drillPageDriver.StartDrill(drillCount);

                for (int i = 0; i < drillCount; i++)
                { 
                    VerifyAnswerToQuestion(drillPageDriver); 
                    drillPageDriver.LoadNextPage(); 
                }

                //System.Threading.Thread.Sleep(10000);
            }
        }

        void VerifyAnswerToQuestion(SozysozbotHandDrillPageDriver drillPageDriver)
        {
            IReadOnlyPiece[] questionPieces = drillPageDriver.LoadQuestion();
            IHand[] hands = solveFunction(questionPieces);
            drillPageDriver.PostAnswers(hands);
            drillPageDriver.DecideAnswer();

            if (!drillPageDriver.IsCorrect()) drillPageDriver.ShowErrorMessage(questionPieces);
        }
    }
}