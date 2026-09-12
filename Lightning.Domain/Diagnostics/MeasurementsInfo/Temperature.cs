using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;

namespace Lighting.Domain.Diagnostics.MeasurementsInfo
{
	public class Temperature : MeasurementInfo<double>, IComparable<Temperature>
	{
		public Temperature() : base(default) { }
		public Temperature(double value) : base(value) { }

		public double Celsius { get => RawValue; }

		public int CompareTo(Temperature? other) => base.ComprareTo(other);

		public static implicit operator double(Temperature temperature) => temperature is null ? default : temperature.RawValue;
		
		public override string ToString()
		{
			return RawValue.ToString();
		}
	}
}
