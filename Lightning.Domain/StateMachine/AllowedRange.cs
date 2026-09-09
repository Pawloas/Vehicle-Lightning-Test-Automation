using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Lighting.Domain.Diagnostics.MeasurementsInfo;

namespace Lighting.Domain.StateMachine
{
	public class AllowedRange<T> where T : notnull, IComparable<T>
	{
		public T MinRange { get; set; }
		public T MaxRange { get; set; }

		public AllowedRange(T minRange, T maxRange)
		{
			MinRange = minRange;
			MaxRange = maxRange;
		}

		public bool IsInRange(T meanValue)
		{
			bool meanValueAfterMinRange = meanValue.CompareTo(MinRange) >= 0;
			bool meanValueBeforeMaxRange = MaxRange.CompareTo(meanValue) >= 0;

			return meanValueAfterMinRange && meanValueBeforeMaxRange;
		}
	}
}
