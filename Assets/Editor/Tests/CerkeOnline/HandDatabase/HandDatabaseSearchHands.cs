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
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.Official;
using Azarashi.CerkeOnline.Domain.Entities.Official.Pieces;

namespace Azarashi.CerkeOnline.Tests
{
    public class HandDatabaseSearchHands
    {
        [Test]
        public void HandDatabaseSearchHandsSimplePasses()
        {
            TestMethod(new HandDatabase(BoardFactory.Create(new Player(Terminologies.Encampment.Front), new Player(Terminologies.Encampment.Back)),new UniRx.Subject<UniRx.Unit>()));
        }

        void TestMethod(IHandDatabase handDatabase)
        {
            new SozysozbotHandQuestionEngine(handDatabase.SearchHands).Solve();
        }
    }
}
