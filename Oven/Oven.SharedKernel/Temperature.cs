using System;

namespace Oven
{
	[Serializable]
	public struct Temperature : IEquatable<Temperature>, IComparable<Temperature>
	{
		private readonly uint _kelvin;
		private Temperature (uint kelvin)
		{
			_kelvin = kelvin;
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
	}
}

