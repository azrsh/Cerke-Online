using System;
using OpenQA.Selenium.Chrome;
using Azarashi.CerkeOnline.Domain.Entities;

namespace Azarashi.CerkeOnline.Tests
{
    public class SozysozbotHandVerificater
    {
        readonly Func<IReadOnlyPiece[], IHand[]> solveFunction;
        readonly int drillCount;
        
        public SozysozbotHandVerificater(Func<IReadOnlyPiece[], IHand[]> solveFunction, int drillCount = 50)
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
            IHand[] hands = solveFunction(questionPieces);
            drillPageDriver.PostAnswers(hands);
            drillPageDriver.DecideAnswer();

            if (!drillPageDriver.IsCorrect()) drillPageDriver.ShowErrorMessage(questionPieces);
        }
    }
}