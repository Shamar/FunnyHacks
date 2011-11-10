using System;
using Oven;
using Oven.Reporting;
using System.Collections.Generic;

namespace iOven
{
    public interface IReporter : ITimer
    {
        IEnumerable<Report> Reports { get; }
        
        void SetReportInterval(Minute interval);
		
		event EventHandler<InfoEventArgs<OvenUri>> NewReportCreated;
    }
}

