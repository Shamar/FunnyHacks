using System;
using Oven;
using Oven.Reporting;

namespace iOven
{
	[Serializable]
	public sealed class Report
	{
		private readonly Time _time; 
		private readonly IState _state;
		
		public Report (Time time, IState state)
		{
			if(null == state)
				throw new ArgumentNullException("state");
			_time = time;
			_state = state;
		}
		
		public Time Time { get {return _time;} }
		
		public IState State { get {return _state;} }
	}
}

