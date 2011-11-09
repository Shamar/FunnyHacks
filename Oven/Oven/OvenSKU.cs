using System;

namespace Oven
{
    [Serializable]
    public sealed class OvenSKU : IEquatable<OvenSKU>
    {
        private static uint _max = 0;
        private readonly uint _current;
        public OvenSKU()
        {
            _current = _max++;
        }
        
        #region IEquatable[OwenSKU] implementation
        public bool Equals (OvenSKU other)
        {
            return _current == other._current;
        }
        #endregion
    }
}

