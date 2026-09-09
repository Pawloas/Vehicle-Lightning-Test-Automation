using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Lighting.Domain.Diagnostics.MeasurementsInfo
{
	public abstract class MeasurementInfo<T> where T : notnull, IComparable<T>, System.Numerics.INumber<T>
	{
		protected T RawValue { get; set; }

		protected MeasurementInfo(T value)
		{
			RawValue = value;
		}

		public bool ComprareTo(MeasurementInfo<T> other)
		{
			return RawValue.CompareTo(other.RawValue) == 0;
		}
	}
}
