using System;
using Oven;
using Oven.Control;

namespace iOven
{
	[Serializable]
	public sealed class AlarmBell : IAlarmBell
	{
		private readonly TimerId _id;
		
		private Time? _startTime;
		private Minute _interval;
		
		public AlarmBell (Uri cookUri)
		{
			if(null == cookUri)
				throw new ArgumentNullException("cookUri");
			_id = new TimerId(cookUri.MakeRelativeUri(new Uri("alarm/")).ToString());
		}
		
		private void FireEvent(EventHandler<InfoEventArgs<Time>> handler, Time time)
		{
			if(null != handler)
				handler(this, new InfoEventArgs<Time>(time));
		}

		#region IAlarmBell implementation
		public event EventHandler<InfoEventArgs<Time>> Ringing;

		public event EventHandler<InfoEventArgs<Time>> Muting;

		public void RingAtEach (Minute interval)
		{
			_interval = interval;
		}

		public bool IsRinging
		{
			get
			{
				throw new NotImplementedException ();
			}
		}
		#endregion

		#region ITimer implementation
		public event EventHandler<InfoEventArgs<Time>> Started;

		public event EventHandler<InfoEventArgs<Time>> Stopped;

		public void StartAt (Time time)
		{
			bool started = !_startTime.HasValue && time < Time.Now;
			_startTime = time;
			if(started)
				FireEvent(Started, time);
		}

		public void StopAt (Time time)
		{
			if(_startTime.HasValue && _startTime.Value > time)
				throw new ArgumentOutOfRangeException("time");
			
			_startTime = null;
			FireEvent(Stopped, time);
		}

		public TimerId Address
		{
			get
			{
				return _id;
			}
		}

		public Minute? Elapsed
		{
			get
			{
				if(!_startTime.HasValue)
					return null;
				return Time.Now - _startTime.Value;
			}
		}
		#endregion
	}
}

