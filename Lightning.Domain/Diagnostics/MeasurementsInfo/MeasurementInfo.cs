using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Numerics;

namespace Lighting.Domain.Diagnostics.MeasurementsInfo
{
	public abstract class MeasurementInfo<T> where T : notnull, IComparable<T>, INumber<T>
	{
		protected T RawValue { get; set; }

		protected MeasurementInfo(T value)
		{
			RawValue = value;
		}
		
		public int ComprareTo(MeasurementInfo<T>? other)
		{
			ArgumentNullException.ThrowIfNull(other);

			return RawValue.CompareTo(other.RawValue);
		}
	}
}
