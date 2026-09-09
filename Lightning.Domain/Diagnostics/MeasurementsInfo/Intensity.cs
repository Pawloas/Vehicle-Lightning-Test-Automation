using System;
using System.Collections.Generic;
using System.Text;

namespace Lighting.Domain.Diagnostics.MeasurementsInfo
{
	public class Intensity : MeasurementInfo<double>, IComparable<Intensity>
	{
		public Intensity() : base(default) { }
		public Intensity(double value) : base(value) { }

		public double Percentages { get => RawValue; }

		public int CompareTo(Intensity? other)
		{
			if (other is null) return 1;
			return RawValue.CompareTo(other.RawValue);
		}

		public static implicit operator int(Intensity i) => i is null ? default : (int)i.RawValue;

		public override string ToString()
		{
			return RawValue.ToString();
		}
	}
}
