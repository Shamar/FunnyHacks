using System;
using System.Collections.Generic;

namespace Poker
{
    /// <summary>
    /// This is the unique identifier of the card deck (something like an EAN or UPC, but quick&dirty).
    /// </summary>
    public sealed class SerialNumber : IEquatable<SerialNumber>
    {
        private readonly Guid _guid = Guid.NewGuid(); // just to be fast.. :-D
        
        #region IEquatable[SerialNumber] implementation
        public bool Equals (SerialNumber other)
        {
            if(null == other)
                return false;
            return _guid.Equals(other._guid);
        }
        #endregion
        
        public override bool Equals (object obj)
        {
            return Equals (obj as SerialNumber);
        }
        
        public override int GetHashCode ()
        {
            return _guid.GetHashCode ();
        }
        
        public override string ToString ()
        {
            return _guid.ToString();
        }
    }
    
    public interface IDeck : IEnumerable<Card>
    {
        SerialNumber Serial { get; }
        
        void Shuffle(DateTime when);
        
        event EventHandler<ShuffleEventArg> Shuffled;
    }
    
    public sealed class ShuffleEventArg : EventArgs
    {
        public ShuffleEventArg(DateTime when)
        {
            When = when;
        }
        
        public readonly DateTime When;
    }
}

