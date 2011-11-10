using System;
using Oven;
using Oven.Reporting;
using System.Collections.Generic;

namespace iOven
{
    public interface IReporter : ITimer
    {
        IEnumerable<IReport> Reports { get; }
        
        void Observe(Oven.Control.IOven oven, Minute interval);
    }
}

