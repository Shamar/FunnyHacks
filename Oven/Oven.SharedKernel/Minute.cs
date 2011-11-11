using System;

namespace Oven
{
    [Serializable]
    public struct Minute : IEquatable<Minute>
    {
        private readonly int _quantity;
        public Minute(int quantity)
        {
            _quantity = quantity;
        }
        
        public TimeSpan AsTimeSpan() { return new TimeSpan(0, _quantity, 0); }
        
        #region IEquatable[Cycle] implementation
        public bool Equals (Minute other)
        {
            return _quantity.Equals(other._quantity);
        }
        #endregion
  
        public override string ToString ()
        {
            if(_quantity < 2)
                return string.Format ("{0} minute", _quantity);
            else
                return string.Format ("{0} minutes", _quantity);
        }

		public override bool Equals (object obj)
		{
			if(obj is Time)
				return Equals((Minute)obj);
			return false;
		}
		
		public override int GetHashCode ()
		{
			return _quantity;
		}
    }
}

