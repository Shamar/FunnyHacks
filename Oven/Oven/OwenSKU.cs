using System;

namespace Oven
{
    [Serializable]
    public sealed class OwenSKU : IEquatable<OwenSKU>
    {
        private static uint _max = 0;
        private readonly int _current;
        public OwenSKU()
        {
            _current = _max++;
        }
        
        #region IEquatable[OwenSKU] implementation
        public bool Equals (OwenSKU other)
        {
            return _current == other._current;
        }
        #endregion
    }
}

