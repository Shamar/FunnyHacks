using System;

namespace Oven.Control
{
    public interface IAlarmBell : ITimer
    {
        void RingAtEach(Minute interval, Minute duration);
        
        bool IsRinging { get; }
        
        event EventHandler<InfoEventArgs<Time>> Ringing;
        
        event EventHandler<InfoEventArgs<Time>> Muting;
    }
}

