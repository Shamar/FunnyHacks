using System;

namespace Oven
{
    [Serializable]
    public sealed class Cycle 
    {
        private readonly DateTime _time;
        private readonly TimeSpan _span;

        public Cycle(TimeSpan span, DateTime start)
        {
            _span = span;
            _time = start;
        }
        
        
    }
}

