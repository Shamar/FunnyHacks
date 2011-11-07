using System;
using System.Collections.Generic;

namespace Poker
{
    public sealed class Deck : IDeck
    {
        #region static stuffs
        private static readonly Card[] _allCards;
        static Deck()
        {
            List<Card> cards = new List<Card>();
            foreach(Suits s in Enum.GetValues(typeof(Suits)))
            {
                foreach(Values v in Enum.GetValues(typeof(Values)))
                    cards.Add(new Card(v, s));
            }
            _allCards = cards.ToArray();
        }
        
        public static Card[] Shuffle(Random random)
        {
            Card[] array = (Card[])_allCards.Clone();
            for (int i = array.Length; i > 1; i--)
            {
                int j = random.Next(i);
                Card tmp = array[j];
                array[j] = array[i - 1];
                array[i - 1] = tmp;
            }
            return array;
        }
        #endregion static stuffs
        
        public Deck (SerialNumber serial)
        {
            if(null == serial)
                throw new ArgumentNullException("serial");
            _serial = serial;
        }
        
        private readonly SerialNumber _serial;
        private DateTime? _lastShuffleTime;
        
        private void FireShuffled(DateTime? previousShuffleTime, DateTime when)
        {
            if(previousShuffleTime.HasValue && previousShuffleTime.Value != when)
            {
                EventHandler<ShuffleEventArg> handler = Shuffled;
                if(null != handler)
                    handler(this, new ShuffleEventArg(when));
            }
        }
        
        #region IDeck implementation
        public SerialNumber Serial
        {
            get { return _serial; }
        }
        
        public void Shuffle (DateTime when)
        {
            DateTime? previousShuffleTime = _lastShuffleTime;
            
            _lastShuffleTime = when;
            
            FireShuffled(previousShuffleTime, when);
        }
        
        public event EventHandler<ShuffleEventArg> Shuffled;
        #endregion

        #region IEnumerable[Poker.Card] implementation
        public IEnumerator<Card> GetEnumerator ()
        {
            if(!_lastShuffleTime.HasValue)
                return ((IEnumerable<Card>)_allCards).GetEnumerator();
            
            IEnumerable<Card> shuffledCards = Shuffle(new Random(((int)_lastShuffleTime.Value.Ticks) ^ _serial.GetHashCode()));
            return shuffledCards.GetEnumerator();
        }
        #endregion

        #region IEnumerable implementation
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
        {
            return GetEnumerator();
        }
        #endregion
    }
}

