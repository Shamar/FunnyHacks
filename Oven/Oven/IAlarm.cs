using System;

namespace Oven
{
    public interface IAlarm
    {
        OvenSKU Oven { get; }
        
        
        
        event EventHandler<EventArgs> Raising;
    }
}

