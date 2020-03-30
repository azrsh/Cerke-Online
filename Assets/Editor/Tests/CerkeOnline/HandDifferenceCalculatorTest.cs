using System;
using System.Linq;
using NUnit.Framework;
using Azarashi.CerkeOnline.Domain.Entities;
using Azarashi.CerkeOnline.Domain.Entities.StandardizedRule.Hands.PieceStackProviders;

namespace Azarashi.CerkeOnline.Tests
{
    public class HandDifferenceCalculatorTest
    {
        [Test]
        public void ArgumentErrorThrowCheck()
        {
            Assert.Throws<ArgumentNullException>(() => HandDifferenceCalculator.Calculate(null, Enumerable.Empty<IHand>()));
            Assert.Throws<ArgumentNullException>(() => HandDifferenceCalculator.Calculate(Enumerable.Empty<IHand>(), null));
            Assert.Throws<ArgumentNullException>(() => HandDifferenceCalculator.Calculate(null, null));
        }

        [Test]
        public void Case1()
        {
            var difference = new IHand[] { new DefaultHand(new LaElmss(), 0) };
            var previous = new IHand[] { new DefaultHand(new LaAls(), 0) };
            var next = previous.Concat(difference);

            var differenceHandName = difference.Select(hand => hand.Name);

            var result = HandDifferenceCalculator.Calculate(previous, next);
            Assert.IsFalse(result.DecreasedDifference.Any()); 
            Assert.IsTrue(
                result.IncreasedDifference
                .Select(hand => hand.Name)
                .OrderBy(name => name)
                .SequenceEqual(differenceHandName)
                );
        }

        [Test]
        public void Case2()
        {
            var difference = new IHand[] { new DefaultHand(new LaElmss(), 0) };
            var next = new IHand[] { new DefaultHand(new LaNermetixaler(), 0) };
            var previous = next.Concat(difference);

            var differenceHandName = difference.Select(hand => hand.Name).OrderBy(name => name);

            var result = HandDifferenceCalculator.Calculate(previous, next);
            Assert.IsFalse(result.IncreasedDifference.Any());
            Assert.IsTrue(
                result.DecreasedDifference
                .Select(hand => hand.Name)
                .OrderBy(name => name)
                .SequenceEqual(differenceHandName)
                );
        }
    }
}
