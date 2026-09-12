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

		public int CompareTo(Voltage? other) => base.ComprareTo(other);

		public static implicit operator double(Voltage voltage) => voltage is null ? default : voltage.RawValue;

		public override string ToString()
		{
			return RawValue.ToString();
		}
	}
}
