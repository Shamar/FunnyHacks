using System;

namespace Oven
{
	[Serializable]
	public struct Temperature : IEquatable<Temperature>, IComparable<Temperature>
	{
		private readonly double _kelvin;
		private Temperature (double kelvin)
		{
			_kelvin = kelvin;
		}
		
		public static Temperature FromCelsius(double value)
		{
			return new Temperature(value + 273.15);
		}
		
		public static Temperature FromFahrenheit(double value)
		{
			return new Temperature((value + 459.67) * 5 / 9);
		}
		
		#region IEquatable[Temperature] implementation
		public bool Equals (Temperature other)
		{
			return _kelvin.Equals(other._kelvin);
		}
		#endregion

		#region IComparable[Temperature] implementation
		public int CompareTo (Temperature other)
		{
			return _kelvin.CompareTo(other._kelvin);
		}
		#endregion
		
		public double Celsius
		{
			get
			{
				return _kelvin - 273.15;
			}
		}
		
		public double Fahrenheit 
		{
			get
			{
				return (_kelvin * 9 / 5) - 459.67;
			}
		}
		
		public override bool Equals (object obj)
		{
			if(obj is Temperature)
				return Equals((Temperature)obj);
			return false;
		}
		
		public override int GetHashCode ()
		{
			return _kelvin.GetHashCode();
		}
	}
}

