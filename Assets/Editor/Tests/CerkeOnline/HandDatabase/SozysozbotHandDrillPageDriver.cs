/*using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using UnityEngine;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Pieces;

namespace Azarashi.CerkeOnline.Tests.HandDatabaseTest
{
    internal class SozysozbotHandDrillPageDriver
    {
        const string url = "https://sozysozbot.github.io/cerke_calculate_hands/calculate_hand_contest.html";

        readonly RemoteWebDriver driver;

        public SozysozbotHandDrillPageDriver(RemoteWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
        }

        public void StartDrill(int drillCount)
        {
            IWebElement contestElement = driver.FindElementById("contest");
            contestElement.FindElements(By.TagName("input"))
                .Where(button => button.GetAttribute("value") == drillCount.ToString() + "本ノックを始める").First().Click();
        }

        public IReadOnlyPiece[] LoadQuestion()
        {
            string questionText = GetQuestionText(driver);
            string[] questionElementsText = SplitQuestionText(questionText);
            return ParseQuestionTextArrayToPieceArray(questionElementsText);
        }

        string GetQuestionText(RemoteWebDriver driver) => driver.FindElementById("contest").Text.Split('\n')[2];

        string[] SplitQuestionText(string questionText) => questionText.Split(new string[] { "、" }, StringSplitOptions.None);

        IReadOnlyPiece[] ParseQuestionTextArrayToPieceArray(string[] questionElementsText) =>
            questionElementsText.Select<string, (string color, string kind)>(text => (text[0].ToString(), text[1].ToString()))
                .Select(pair => (IReadOnlyPiece)ConvertPieceNameToPieceInstance(ConvertColorTextToColorId(pair.color), pair.kind)).ToArray();

        public void PostAnswers(IEnumerable<IHand> answers)
        {
            IReadOnlyCollection<IWebElement> labelElements = driver.FindElementsByTagName("label");
            foreach (IHand hand in answers)
            {
                string elementTextStyleHandName = TranslateHandNameToElementTextStyle(hand.Name);
                IWebElement handTextElement = labelElements.Where(element => element.Text == elementTextStyleHandName).FirstOrDefault();

                if (handTextElement == null) { Debug.LogError("webElement is null! : " + hand.Name); continue; }

                string radioButtoElementId = handTextElement.GetAttribute("for");
                driver.FindElementById(radioButtoElementId).Click();
            }
        }

        public void DecideAnswer()
        {
            IWebElement decisionButton = driver.FindElementsByTagName("input").Where(element => element.GetAttribute("type") == "button")
                .Where(element => element.GetAttribute("value") == "決定").FirstOrDefault();
            decisionButton.Click();
        }

        //Pageじゃない方がいいかも（表示してるhtmlファイルは同じなので）
        public void LoadNextPage()
        {
            IWebElement okButton = driver.FindElementsByTagName("input").Where(element => element.GetAttribute("type") == "button")
                  .Where(element => element.GetAttribute("value") == "OK").FirstOrDefault();
            okButton.Click();
        }

        public bool IsCorrect() => driver.FindElementByXPath("//*[@id=\"result\"]/strong").Text == "正解!";

        public void ShowErrorMessage(IReadOnlyPiece[] questionPieces) =>
            Debug.LogError("不正解\n" + questionPieces.Select(piece => piece.PieceName.ToString()) + "\n");

        Terminologies.PieceColor ConvertColorTextToColorId(string colorText)
        {
            switch (colorText)
            {
                case "黒":
                    return Terminologies.PieceColor.Black;
                case "赤":
                    return Terminologies.PieceColor.Red;
                default:
                    throw new ArgumentException("ColorText");
            }
        }

        IPiece ConvertPieceNameToPieceInstance(Terminologies.PieceColor color, string pieceName)
        {
            switch (pieceName)
            {
                case Terminologies.Pieces.Felkana:
                    return new Felkana(color, default, default, default);
                case Terminologies.Pieces.Elmer:
                    return new Elmer(color, default, default, default);
                case Terminologies.Pieces.Gustuer:
                    return new Gustuer(color, default, default, default);
                case Terminologies.Pieces.Vadyrd:
                    return new Vadyrd(color, default, default, default);
                case Terminologies.Pieces.Stistyst:
                    return new Stistyst(color, default, default, default);
                case Terminologies.Pieces.Dodor:
                    return new Dodor(color, default, default, default);
                case Terminologies.Pieces.Kua:
                    return new Kua(color, default, default, default);
                case Terminologies.Pieces.Terlsk:
                    return new Terlsk(color, default, default, default);
                case Terminologies.Pieces.Varxle:
                    return new Varxle(color, default, default, default);
                case Terminologies.Pieces.Ales:
                    return new Ales(color, default, default, default);
                case Terminologies.Pieces.Tam:
                    return new Tam(color, default, default, default);
                default:
                    throw new ArgumentException("piece name");
            }
        }

        string TranslateHandNameToElementTextStyle(string handName)
        {
            if (handName == "王" || handName == "同色王")
                return "王 = 同色王";

            return handName;
        }
    }
}*/