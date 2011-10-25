using System;
using System.Linq;
using NUnit.Framework;

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
    }
}

