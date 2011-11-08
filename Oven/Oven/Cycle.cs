using System;

namespace Oven
{
    [Serializable]
    public sealed class Cycle 
    {
        private readonly DateTime _start;
        private readonly TimeSpan _span;
		private readonly uint _count = 0;

        public Cycle(TimeSpan span, DateTime start)
        {
            _span = span;
            _start = start;
        }
		
		private Cycle(Cycle previous)
		{
			_start = previous._start;
			_span = previous._span;
			_count = previous._count + 1;
		}
        
        public Cycle Next
		{
			get {return new Cycle(this);}
		}
		
		public Cycle At(DateTime time)
		{
		}
    }
}

