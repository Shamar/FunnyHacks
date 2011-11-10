using System;

namespace Oven
{
	[Serializable]
    public class InfoEventArgs<T> : EventArgs
    {
        public readonly T Value;
        public InfoEventArgs (T value)
        {
            Value = value;
        }
    }
}

