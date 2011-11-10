using System;

namespace Oven.Reporting
{
    public interface IOven : Oven.IOven
    {
        IState CurrentState { get; }
    }
}

