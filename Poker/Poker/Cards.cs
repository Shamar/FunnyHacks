using System;

namespace Poker
{
    public sealed class Card : IEquatable<Card>
    {
        public Card(Values value, Suits suit)
        {
            Value = value;
            Suit = suit;
        }
        
        public readonly Values Value;
        
        public readonly Suits Suit;

        #region IEquatable[Card] implementation
        public bool Equals (Card other)
        {
            if(null == other)
                return false;
            return Value == other.Value && Suit == other.Suit;
        }
        #endregion
        
        public override bool Equals (object obj)
        {
            return Equals (obj as Card);
        }
        
        public override int GetHashCode ()
        {
            return (int)Value;
        }
        
        public override string ToString ()
        {
            return string.Format ("{0} of {1}", Enum.GetName(typeof(Values), Value), Enum.GetName(typeof(Suits), Suit));
        }
    }

    public enum Suits
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }
    
    public enum Values
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
}

