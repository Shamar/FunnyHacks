using System;

namespace Oven
{
    public interface IAlarmBell : ITimer
    {
        void RingAtEach(Minute interval);
        
        bool IsRinging { get; }
        
        event EventHandler<EventArgs> Ringing;
        
        event EventHandler<EventArgs> Muting;
    }
}

