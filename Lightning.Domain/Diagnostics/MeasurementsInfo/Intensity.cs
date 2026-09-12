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

		public int CompareTo(Intensity? other) => base.ComprareTo(other);

		public static implicit operator double(Intensity intensity) => intensity is null ? default : intensity.RawValue;

		public override string ToString()
		{
			return RawValue.ToString();
		}
	}
}
