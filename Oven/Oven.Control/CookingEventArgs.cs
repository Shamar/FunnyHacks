using System;

namespace Oven.Control
{
	[Serializable]
	public sealed class CookingEventArgs : EventArgs
	{
		public readonly Time Time;
		public readonly Temperature Temperature;
		public readonly Minute PlannedConclusion;
		
		public CookingEventArgs (Time time, Temperature temperature, Minute plannedConclusion)
		{
			Time = time;
			Temperature = temperature;
			PlannedConclusion = plannedConclusion;
		}
	}
}

