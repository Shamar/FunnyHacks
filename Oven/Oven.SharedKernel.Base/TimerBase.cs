using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Oven;
using ThreadingTimer = System.Threading.Timer;
using TimerCallback = System.Threading.TimerCallback;

namespace Oven
{
	[Serializable]
	public abstract class TimerBase : ITimer, ISerializable
	{
		private readonly TimerId _id;

		private readonly Dictionary<Minute, Action> _schedules;
		private readonly Dictionary<Minute, ThreadingTimer> _timers;
		
		private readonly TimerCallback _timerDelegate;

		private Time? _startTime;
		private Minute _interval;
		
		protected TimerBase (Uri uri)
		{
			if(null == uri)
				throw new ArgumentNullException("uri");
			_id = new TimerId(uri.ToString());
			_schedules = new Dictionary<Minute, Action>();
			_timers = new Dictionary<Minute, ThreadingTimer>();
			_timerDelegate = new TimerCallback(this.Execute);
		}
		
		protected bool IsStarted
		{
			get { return _startTime.HasValue; }
		}
		
		protected void FireEvent(EventHandler<InfoEventArgs<Time>> handler, Time time)
		{
			if(null != handler)
				handler(this, new InfoEventArgs<Time>(time));
		}
		
		protected void EmptySchedules()
		{
			StopSchedules();
			_schedules.Clear();
		}
		
		private void StartSchedules()
		{
			if(IsStarted)
			{
				foreach(Minute interval in _schedules.Keys)
					AddTimer(interval);
			}
		}
		
		private void StopSchedules()
		{
			if(IsStarted)
			{
				foreach(Minute interval in _schedules.Keys)
					RemoveTimer(interval);
			}
		}
		
		private void AddTimer(Minute interval)
		{
			long startDateTicks = _startTime.Value.AsUtc().Ticks;
			long span = interval.AsTimeSpan().Ticks;
			long cycles;
			long remainder = Math.DivRem(DateTime.UtcNow.Ticks - startDateTicks, span, out cycles);
			long dueTime = startDateTicks + (span * (cycles + 1)) - remainder;
			
			_timers[interval] = new ThreadingTimer(_timerDelegate, interval, dueTime, span);
		}
		
		private void RemoveTimer(Minute interval)
		{
			ThreadingTimer timer = _timers[interval];
			timer.Dispose();
			_timers.Remove(interval);
		}
		
		private void AddAction(Minute interval, Action action)
		{
			Action scheduled = null;
			if(!_schedules.TryGetValue(interval, out scheduled))
			{
				scheduled = action;
				if(IsStarted)
					AddTimer(interval);
			}
			else
			{
				scheduled = (Action)Delegate.Combine(scheduled, action);
			}
			_schedules[interval] = scheduled;
		}
		
		private void RemoveAction(Minute interval, Action action)
		{
			Action scheduled = null;
			if(_schedules.TryGetValue(interval, out scheduled))
			{
				if(scheduled.GetInvocationList().Length == 1)
				{
					_schedules.Remove(interval);
					if(IsStarted)
						RemoveTimer(interval);
				}
				else
				{
					scheduled = (Action)Delegate.Remove(scheduled, action);
					_schedules[interval] = scheduled;
				}
			}
		}
		
		private void Execute(Object stateInfo)
		{
			Minute interval = (Minute)stateInfo;
			Action action = _schedules[interval];
			action();
		}
		
		protected void AddPeriodicTask(Minute interval, Action action)
		{
			if(null == action)
				throw new ArgumentNullException("action");
			if(null != action.Target && this != action.Target)
				throw new ArgumentException("The action must be either a static method or a method of this.","action");
			AddAction(interval, action);
		}
		
		protected void RemovePeriodicTask(Minute interval, Action action)
		{
			if(null == action)
				throw new ArgumentNullException("action");
			if(null != action.Target && this != action.Target)
				throw new ArgumentException("The action must be either a static method or a method of this.","action");
			RemoveAction(interval, action);
		}
		
		#region ITimer implementation
		public event EventHandler<InfoEventArgs<Time>> Started;

		public event EventHandler<InfoEventArgs<Time>> Stopped;

		public void StartAt (Time time)
		{
			bool started = !_startTime.HasValue && time < Time.Now;
			_startTime = time;
			if(started)
			{
				StartSchedules();
				FireEvent(Started, time);
			}
		}

		public void StopAt (Time time)
		{
			if(_startTime.HasValue && _startTime.Value > time)
				throw new ArgumentOutOfRangeException("time");
			
			bool stopped = _startTime.HasValue;
			_startTime = null;
			if(stopped)
			{
				StopSchedules();
				FireEvent(Stopped, time);
			}
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
		#endregion ITimer implementation
		
		#region ISerializable implementation
		protected TimerBase(SerializationInfo info, StreamingContext context)
		{
			_id = (TimerId)info.GetValue("id", typeof(TimerId));
			_startTime = (Time?)info.GetValue("t", typeof(Time?));
			_interval = (Minute)info.GetValue("i", typeof(Minute));
			_schedules = (Dictionary<Minute, Action>)info.GetValue("s", typeof(Dictionary<Minute, Action>));
			_timerDelegate = new TimerCallback(this.Execute);
		}
		
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("id", _id);
			info.AddValue("t", _startTime);
			info.AddValue("i", _interval);
			info.AddValue("s", _schedules);
		}
		
		void ISerializable.GetObjectData (SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}
		#endregion ISerializable implementation
	}
}

