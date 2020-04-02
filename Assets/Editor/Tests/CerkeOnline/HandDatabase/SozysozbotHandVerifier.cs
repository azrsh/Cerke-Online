/*using System;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Tests.HandDatabaseTest
{
    internal class SozysozbotHandVerifier
    {
        readonly Func<IReadOnlyPiece[], IEnumerable<IHand>> solveFunction;
        readonly int drillCount;
        
        public SozysozbotHandVerifier(Func<IReadOnlyPiece[], IEnumerable<IHand>> solveFunction, int drillCount = 50)
        {
            this.solveFunction = solveFunction;
            this.drillCount = drillCount;
        }

        public void Verify()
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
                    VerifyEachCase(drillPageDriver); 
                    drillPageDriver.LoadNextPage(); 
                }

                //System.Threading.Thread.Sleep(10000);
            }
        }

        void VerifyEachCase(SozysozbotHandDrillPageDriver drillPageDriver)
        {
            IReadOnlyPiece[] questionPieces = drillPageDriver.LoadQuestion();
            IEnumerable<IHand> hands = solveFunction(questionPieces);
            drillPageDriver.PostAnswers(hands);
            drillPageDriver.DecideAnswer();

            if (!drillPageDriver.IsCorrect()) drillPageDriver.ShowErrorMessage(questionPieces);
        }
    }
}*/