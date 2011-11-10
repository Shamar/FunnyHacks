using System;

namespace Oven
{
    public interface ITimer
    {
        TimerId Address { get; }
        
        Minute? Elapsed { get; }
        
        void StartAt(Time date);
        
        void StopAt(Time date);
        
        event EventHandler<InfoEventArgs<Time>> Started;
        
        event EventHandler<InfoEventArgs<Time>> Stopped;
    }
}

