using System;
using System.IO;

namespace Oven.Reporting
{
    public interface IReport
    {
        OvenUri Oven { get; }
        
        Time Time { get; }
        
        void WriteTo(Stream stream);
    }
}

