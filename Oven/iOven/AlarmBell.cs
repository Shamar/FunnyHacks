using System;
using Oven;
using Oven.Control;
using System.Runtime.Serialization;
using ThreadingTimer = System.Threading.Timer;
using TimerCallback = System.Threading.TimerCallback;
using Timeout = System.Threading.Timeout;

namespace iOven
{
	[Serializable]
	public sealed class AlarmBell : TimerBase, IAlarmBell
	{
		private static Uri GetAlarmUri(Uri cookUri)
		{
			if(null == cookUri)
				return null;
			return cookUri.MakeRelativeUri(new Uri("alarm/"));
		}
		
		private bool _ringing = false;
		
		private ThreadingTimer _timer;
		
		private int _duration;
		
		public AlarmBell (Uri cookUri)
			: base(GetAlarmUri(cookUri))
		{
			_timer = new ThreadingTimer(new TimerCallback(Mute));
		}
		
		

		#region IAlarmBell implementation
		public event EventHandler<InfoEventArgs<Time>> Ringing;

		public event EventHandler<InfoEventArgs<Time>> Muting;

		public void RingAtEach (Minute interval, Minute duration)
		{
			_duration = duration.AsTimeSpan().Milliseconds;
			EmptySchedules();
			base.AddPeriodicTask(interval, Ring);
		}
		
		private void Ring()
		{
			if(!_ringing)
			{
				_ringing = true;
				_timer.Change(_duration, Timeout.Infinite);
				FireEvent(Ringing, Time.Now);
			}
		}
		
		private void Mute(Object state)
		{
			if(_ringing)
			{
				_ringing = false;
				FireEvent(Muting, Time.Now);
			}
		}
		
		public bool IsRinging
		{
			get
			{
				return _ringing;
			}
		}
		#endregion IAlarmBell implementation

		#region ISerializable implementation
		protected AlarmBell(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			_duration = info.GetInt32("d");
		}
		
		protected override void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context);
			info.AddValue("d", _duration);
		}
		#endregion ISerializable implementation

	}
}

