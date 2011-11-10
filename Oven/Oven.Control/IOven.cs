using System;
using System.Collections.Generic;

namespace Oven.Control
{
    public interface IOven : Oven.IOven
    {
        void StartCooking(Minute duration);
        
        void StopCooking();
        
        bool IsCooking { get; }
        
        Time PlannedStop { get; }
        
        IEnumerable<TimerId> SycronizedTimers { get; }
        
        void CookFor(Minute minutes);
        
        void Syncronize(ITimer timer);
        
        void AlertThrough(IAlarmBell alarm);
        
        event EventHandler<InfoEventArgs<TimerId>> TimerSyncronized;
        
        event EventHandler<InfoEventArgs<Minute>> CookingAdjusted;
        
        event EventHandler<InfoEventArgs<Time>> Cooked;
    }
}

