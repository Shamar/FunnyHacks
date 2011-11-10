using System;

namespace Oven
{
    [Serializable]
    public struct Time : IComparable<Time>, IEquatable<Time>
    {
        private readonly DateTime _utc;
        private Time (DateTime utcTime)
        {
            _utc = utcTime.AddSeconds(-utcTime.Second);
        }
        
        public static Time Now
        {
            get
            {
                return new Time(DateTime.UtcNow);
            }
        }
        
        public Time Add(Minute minutes)
        {
            return new Time(_utc.Add(minutes.AsTimeSpan()));
        }
        
        public DateTime AsUtc()
        {
            return _utc;
        }
        
        public DateTime AsLocal()
        {
            return _utc.ToLocalTime();
        }
  
        public override string ToString ()
        {
            return AsLocal().ToString();
        }
        
        public override int GetHashCode ()
        {
            return _utc.GetHashCode ();
        }
        
        #region IComparable[Time] implementation
        public int CompareTo (Time other)
        {
            return _utc.CompareTo(other._utc);
        }
        #endregion

        #region IEquatable[Time] implementation
        public bool Equals (Time other)
        {
            return _utc.Equals(other._utc);
        }
        #endregion
    }
}

