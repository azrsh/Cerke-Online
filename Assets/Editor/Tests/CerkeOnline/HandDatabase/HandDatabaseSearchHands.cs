using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using UnityEngine.TestTools;
using UnityEngine;
using UnityEngine.Networking;
using Azarashi.Utilities.HTML;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.Official;
using Azarashi.CerkeOnline.Domain.Entities.Official.Pieces;

namespace Azarashi.CerkeOnline.Tests
{
    public class HandDatabaseSearchHands
    {
        const string url = "https://sozysozbot.github.io/cerke_calculate_hands/calculate_hand_contest.html";
        const int drillCount = 50;

        [Test]
        public void HandDatabaseSearchHandsSimplePasses()
        {
            TestMethod(new HandDatabase(BoardFactory.Create(new Player(Terminologies.Encampment.Front), new Player(Terminologies.Encampment.Back)),new UniRx.Subject<UniRx.Unit>()));
        }

        void TestMethod(IHandDatabase handDatabase)
        {
            var path = UnityEngine.Application.dataPath + "/Libraries/Selenium";
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            using (ChromeDriver driver = new ChromeDriver(path, options))
            {
                driver.Navigate().GoToUrl(url);
                IWebElement contestElement = driver.FindElementById("contest");
                contestElement.FindElements(By.TagName("input"))
                    .Where(button => button.GetAttribute("value") == drillCount.ToString() + "本ノックを始める").First().Click();

                for (int i = 0; i < drillCount; i++)
                    SolveQuestion(driver);
                

                System.Threading.Thread.Sleep(5000);
            }

            void SolveQuestion(RemoteWebDriver driver)
            {
                string questionText = driver.FindElementById("contest").Text.Split('\n')[2];
                string[] questionElementsText = questionText.Split(new string[] { "、" }, StringSplitOptions.None);
                IReadOnlyPiece[] questionPieces = questionElementsText.Select<string, (string color, string kind)>(text => (text[0].ToString(), text[1].ToString()))
                    .Select(pair => (IReadOnlyPiece)ConvertPieceNameToPieceInstance(ConvertColorTextToColorId(pair.color), pair.kind)).ToArray();

                IHand[] hands = handDatabase.SearchHands(questionPieces);
                IReadOnlyCollection<IWebElement> labelElements = driver.FindElementsByTagName("label");
                foreach (IHand hand in hands)
                {
                    IWebElement handTextElement;
                    if(hand.Name == "王" || hand.Name == "同色王")
                        handTextElement = labelElements.Where(element => element.Text == "王 = 同色王").FirstOrDefault();
                    else
                        handTextElement = labelElements.Where(element => element.Text == hand.Name).FirstOrDefault();

                    if (handTextElement == null) { Debug.LogError("webElement is null! : " + hand.Name); continue; }
                    
                    string radioButtoElementId = handTextElement.GetAttribute("for");
                    driver.FindElementById(radioButtoElementId).Click();
                }

                IWebElement decisionButton = driver.FindElementsByTagName("input").Where(element => element.GetAttribute("type") == "button")
                    .Where(element => element.GetAttribute("value") == "決定").FirstOrDefault();
                decisionButton.Click();

                bool isTrue = driver.FindElementByXPath("//*[@id=\"result\"]/strong").Text == "正解!";
                if (!isTrue) { Debug.LogError("不正解\n" + questionText + "\n"); }

                IWebElement okButton = driver.FindElementsByTagName("input").Where(element => element.GetAttribute("type") == "button")
                    .Where(element => element.GetAttribute("value") == "OK").FirstOrDefault();
                okButton.Click();
            }
        }

        [UnityTest]
        public IEnumerator SozysozbotCalculateHands()
        {   
            /*UnityWebRequest pageRequest = UnityWebRequest.Get(url);
            yield return pageRequest.SendWebRequest();

            if (pageRequest.isNetworkError) Debug.LogError("communication failure");

            HTMLParser parser = new HTMLParser(pageRequest.downloadHandler.text);
            TagList tagList = parser.Parse();
            
            foreach (Tag tag in tagList)
                foreach (KeyValuePair<string, Attribute> item in tag)
                    Debug.Log("TagName : " + tag.Name +
                        " AttributeName : " + item.Value.Name +
                        " AttributeValue : " + item.Value.Value);

            foreach (Tag tag in tagList)
                foreach (KeyValuePair<string, Attribute> item in tag)
                {
                    if (item.Value.Value != "10本ノックを始める") continue;

                    Attribute attribute = null;
                    tag.TryGetValue("onclick", out attribute);

                    if (attribute == null) continue;

                    string jsCode = attribute.Value;
                    Debug.Log(jsCode);

                    UnityWebRequest drillButtonRequest = UnityWebRequest.Post(url, jsCode);
                    yield return drillButtonRequest.SendWebRequest();
                    if (drillButtonRequest.isNetworkError) Debug.LogError("communication failure");

                    Debug.Log(drillButtonRequest.downloadHandler.text);
                }
            */

            yield return null;
        }

        int ConvertColorTextToColorId(string colorText)
        {
            switch (colorText)
            {
                case "黒":
                    return 0;
                case "赤":
                    return 1;
                default:
                    throw new ArgumentException("ColorText");
            }
        }

        IPiece ConvertPieceNameToPieceInstance(int colorId, string pieceName)
        {
            switch (pieceName)
            {
                case Terminologies.Pieces.Felkana:
                    return new Felkana(colorId, default, default, default);
                case Terminologies.Pieces.Elmer:
                    return new Elmer(colorId, default, default, default);
                case Terminologies.Pieces.Gustuer:
                    return new Gustuer(colorId, default, default, default);
                case Terminologies.Pieces.Vadyrd:
                    return new Vadyrd(colorId, default, default, default);
                case Terminologies.Pieces.Stistyst:
                    return new Stistyst(colorId, default, default, default);
                case Terminologies.Pieces.Dodor:
                    return new Dodor(colorId, default, default, default);
                case Terminologies.Pieces.Kua:
                    return new Kua(colorId, default, default, default);
                case Terminologies.Pieces.Terlsk:
                    return new Terlsk(colorId, default, default, default);
                case Terminologies.Pieces.Varxle:
                    return new Varxle(colorId, default, default, default);
                case Terminologies.Pieces.Ales:
                    return new Ales(colorId, default, default, default);
                case Terminologies.Pieces.Tam:
                    return new Tam(colorId, default, default, default);
                default:
                    throw new ArgumentException("piece name");
            }
        }
    }
}
