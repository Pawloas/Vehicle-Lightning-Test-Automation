using System;
using System.Collections.Generic;
using System.Text;

namespace Lighting.Domain.Diagnostics.MeasurementsInfo
{
	public class Temperature : MeasurementInfo<double>, IComparable<Temperature>
	{
		public Temperature() : base(default) { }
		public Temperature(double value) : base(value) { }

		public double Celsius { get => RawValue; }

		public int CompareTo(Temperature? other)
		{
			if (other is null) return 1;
			return RawValue.CompareTo(other.RawValue);
		}

		public static implicit operator double(Temperature t) => t is null ? default : t.RawValue;

		public override string ToString()
		{
			return RawValue.ToString();
		}
	}
}
