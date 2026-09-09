using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lighting.Domain.Diagnostics.MeasurementsInfo
{
	public class Voltage : MeasurementInfo<double>, IComparable<Voltage>
	{
		public Voltage() : base(default) { }
		public Voltage(double value) : base(value) { }

		public double Volts { get => RawValue; }

		public int CompareTo(Voltage? other)
		{
			if (other is null) return 1;
			return RawValue.CompareTo(other.RawValue);
		}

		public static implicit operator double(Voltage v) => v is null ? default : v.RawValue;

		public override string ToString()
		{
			return RawValue.ToString();
		}
	}
}
