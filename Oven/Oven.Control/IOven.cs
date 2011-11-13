using System;
using System.Collections.Generic;

namespace Oven.Control
{
    public interface IOven : Oven.IOven
    {
        void CookFor(Minute duration, Temperature desiredTemperature);
        
        void StopCooking();
        
        bool IsCooking { get; }
		
		Temperature CurrentTemperature { get; }
        
        Time? PlannedStop { get; }
        
        IEnumerable<TimerId> SycronizedTimers { get; }
        
        void Syncronize(ITimer timer);
        
        void AlertThrough(IAlarmBell alarm);
        
        void Forget(ITimer timer);
		
        event EventHandler<CookingEventArgs> CookingAdjusted;
        
        event EventHandler<InfoEventArgs<Time>> Cooked;
    }
}

