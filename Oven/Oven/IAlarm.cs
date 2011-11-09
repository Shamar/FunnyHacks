using System;

namespace Oven
{
    public interface IAlarm
    {
        Uri Identity { get; }
        
        void RingEach(Cycle cycles);
        
        void StopRinging();

        
        event EventHandler<EventArgs> Ringing;
    }
}

