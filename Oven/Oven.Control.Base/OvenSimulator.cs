using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Control
{
    [Serializable]
    public sealed class OvenSimulator : Oven.Control.IOven
    {
        private readonly OvenUri _uri;
        private Time? _startCooking;
        private Minute _duration;
        private Temperature _temperature;
        
        private readonly HashSet<ITimer> _timers;
        
        public OvenSimulator (OvenUri uri)
        {
            if(null == uri)
                throw new ArgumentNullException("uri");
            _uri = uri;
            _timers = new HashSet<ITimer>(new TimerEqualityComparer());
        }
                
        private void FireEvent<TEventArgs>(EventHandler<TEventArgs> handler, TEventArgs arg)
            where TEventArgs : EventArgs
        {
            if(null != handler)
                handler(this, arg);
        }

        #region IOven implementation
        public event EventHandler<InfoEventArgs<TimerId>> Syncronizing;
		
		public event EventHandler<InfoEventArgs<TimerId>> Forgetting;

        public event EventHandler<CookingEventArgs> CookingAdjusted;

        public event EventHandler<InfoEventArgs<Time>> Cooked;

        public void CookFor (Minute duration, Temperature desiredTemperature)
        {
            bool started = !_startCooking.HasValue;
            _startCooking = Time.Now;
            _duration = duration;
            _temperature = desiredTemperature;
            if(started)
                FireEvent(CookingAdjusted, new CookingEventArgs(_startCooking.Value, desiredTemperature, duration));
        }

        public void StopCooking ()
        {
            bool stopped = _startCooking.HasValue;
            _startCooking = null;
            _duration = new Minute(0);
            _temperature = Temperature.FromCelsius(20);
            if(stopped)
                FireEvent(Cooked, new InfoEventArgs<Time>(_startCooking.Value));
        }

        public void Syncronize (ITimer timer)
        {
            if(_timers.Add(timer))
            {
                FireEvent(Syncronizing, new InfoEventArgs<TimerId>(timer.Address));
                if(_startCooking.HasValue)
                    timer.StartAt(_startCooking.Value);
            }
        }

        public void AlertThrough (IAlarmBell alarm)
        {
            if(_startCooking.HasValue)
            {
                alarm.RingAtEach(new Minute(_duration.AsTimeSpan().Minutes - 2), new Minute(2));
                Syncronize(alarm);
            }
        }
		
		public void Forget(ITimer timer)
		{
			if(_timers.Remove(timer))
			{
				FireEvent(Forgetting, new InfoEventArgs<TimerId>(timer.Address));
				if(_startCooking.HasValue)
					timer.StopAt(Time.Now);
			}
		}

        public bool IsCooking {
            get {
                return _startCooking.HasValue;
            }
        }

        public Temperature CurrentTemperature {
            get {
                return _temperature;
            }
        }

        public Time? PlannedStop {
            get {
                if(_startCooking.HasValue)
                    return _startCooking.Value + _duration;
                return null;
            }
        }

        public IEnumerable<TimerId> SycronizedTimers {
            get {
                return _timers.Select(t => t.Address);
            }
        }
        #endregion

        #region IOven implementation
        public OvenUri Address {
            get {
                return _uri;
            }
        }
        #endregion
		
		#region TimerEqualityComparer
		
		[Serializable]
		class TimerEqualityComparer : IEqualityComparer<ITimer>
		{
			#region IEqualityComparer[ITimer] implementation
			public bool Equals (ITimer x, ITimer y)
			{
				if((null == x) != (null == y))
					return false;
				if(null == x)
					return true;
				return x.Address.Equals(y.Address);
			}

			public int GetHashCode (ITimer obj)
			{
				if(null == obj)
					return 0;
				return obj.Address.GetHashCode();
			}
			#endregion
		}
		
		#endregion TimerEqualityComparer
    }
}

