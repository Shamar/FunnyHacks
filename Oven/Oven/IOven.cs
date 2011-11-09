using System;

namespace Oven
{
    public interface IOven
    {
        OvenSKU SKU { get; }
        
        void TurnOn(int desiredTemperature, DateTime time);
        
        void TurnOff(DateTime time);
        
        void SetCycleDuration(TimeSpan duration);
        
        void SetCookingTime(uint cycles);
        
        void SetReportingInterval(uint cycles);
        
        
    }
}

