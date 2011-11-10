using System;

namespace Oven.Reporting
{
    public interface IOven : Oven.IOven
    {
        IReport CurrentReport { get; }
    }
}

