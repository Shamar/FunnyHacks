using System;

namespace Oven
{
    [Serializable]
    public sealed class Cycle : IEquatable<Cycle>
    {
        private readonly DateTime _start;
        private readonly TimeSpan _duration;
        private readonly uint _quantity = 0;

        public Cycle(DateTime startingTime, TimeSpan duration)
        {
            _start = startingTime;
            _duration = duration;
        }

        private Cycle(DateTime startingTime, TimeSpan duration, uint quantity)
            : this(startingTime, duration)
        {
            _quantity = quantity;
        }
        
        public Cycle Allocate(uint quantity)
        {
            return new Cycle(_start, _duration, quantity);
        }
        
        public TimeSpan TotalDuration { get { return new TimeSpan(_duration.Ticks * _quantity); } }
        
        
        #region IEquatable[Cycle] implementation
        public bool Equals (Cycle other)
        {
            if(null == other)
                return false;
            return _quantity.Equals(other._quantity) && _start.Equals(other._start) && _duration.Equals(other._duration);
        }
        #endregion
    }
}

