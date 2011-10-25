using System;
using System.Linq;
using NUnit.Framework;
using System.Threading;

namespace Poker.UnitTests
{
    [TestFixture()]
    public class DeckQA
    {
        [Test]
        public void Shuffle_withTheSameTimeOnTheSameDeck_isIdempotent()
        {
            // arrange:
            SerialNumber number = new SerialNumber();
            IDeck deck = new Deck(number);
            DateTime time = DateTime.Now;

            // act:
            deck.Shuffle(time);
            Card[] afterFirstShuffle = deck.ToArray();
            deck.Shuffle(time);
            Card[] afterSecondShuffle = deck.ToArray();

            // assert:
            for(int i = 0; i < deck.Count(); ++i)
            {
                Assert.AreSame(afterFirstShuffle[i], afterSecondShuffle[i]);
            }
        }
        
        [Test]
        public void Shuffle_withTheSameTimeOnDifferentDecks_produceDifferentOrders()
        {
            // arrange:
            IDeck firstDeck = new Deck(new SerialNumber());
            IDeck secondDeck = new Deck(new SerialNumber());
            DateTime time = DateTime.Now;
            
            // act:
            firstDeck.Shuffle(time);
            secondDeck.Shuffle(time);

            // assert:
            Assert.IsFalse(firstDeck.SequenceEqual(secondDeck));
        }
        
        [Test]
        public void Shuffle_atDifferentTimes_produceDifferentOrders()
        {
            // arrange:
            SerialNumber number = new SerialNumber();
            IDeck deck = new Deck(number);
            DateTime time = DateTime.Now;

            // act:
            deck.Shuffle(time);
            Card[] afterFirstShuffle = deck.ToArray();
            deck.Shuffle(time + new TimeSpan(100)); // just to be sure :-D
            Card[] afterSecondShuffle = deck.ToArray();

            // assert:
            Assert.IsFalse(afterFirstShuffle.SequenceEqual(afterSecondShuffle));
        }
        
        [Test]
        public void GetEnumerator_afterAShuffle_dontChangeTheCardsOrderOnHisOwn()
        {
            // arrange:
            SerialNumber number = new SerialNumber();
            IDeck deck = new Deck(number);
            DateTime time = DateTime.Now;

            // act:
            deck.Shuffle(time);
            Card[] firstRead = deck.ToArray();
            Thread.Sleep(1000); // just to be sure :-)
            Card[] secondRead = deck.ToArray();

            // assert:
            Assert.IsTrue(firstRead.SequenceEqual(secondRead));
        }
    }
}

