using System;

namespace Oven
{
    public interface IAlarm
    {		
        void Track(Cycle cycle);
        
        event EventHandler<EventArgs> Raising;
    }
}

