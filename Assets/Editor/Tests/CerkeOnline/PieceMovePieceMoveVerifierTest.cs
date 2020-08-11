using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.PieceMoveAction;
using System;
using UniRx;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule;
using Azarashi.CerkeOnline.Domain.Entities.PublicDataType;
using UniRx.Async;

namespace Azarashi.CerkeOnline.Tests
{
    public class PieceMovePieceMoveVerifierTest
    {
        [Test]
        public void PieceMovePieceMoveVerifierTestSimplePasses()
        {
            Assert.Throws<UnityEngine.Assertions.AssertionException>(() => new PieceMoveVerifier(null));

            //var verifier = ;
            //verifier.VerifyMove();
        }
    }
}
