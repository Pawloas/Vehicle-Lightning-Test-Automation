using Lighting.Domain.Diagnostics.MeasurementsInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Lighting.Domain
{
	public static class LightingConstants
	{
		public static readonly Intensity MinIntensity = new Intensity(0);
		public static readonly Intensity ParkingIntensity = new Intensity(20);
		public static readonly Intensity ReducedIntensity = new Intensity(30);
		public static readonly Intensity StandardIntensity = new Intensity(50);
		public static readonly Intensity MaxIntensity = new Intensity(100);


		public static readonly Temperature MinTemperature = new Temperature(-40);
		public static readonly Temperature OperatingTemperature = new Temperature(50);
		public static readonly Temperature WarningTemperature = new Temperature(75);
		public static readonly Temperature HighTresholdTemperature = new Temperature(85);
		public static readonly Temperature MaxTemperature = new Temperature(100);


		public static readonly Voltage CriticalLowVoltage = new Voltage(10.5);
		public static readonly Voltage MinVoltage = new Voltage(11);
		public static readonly Voltage MinStableVoltage = new Voltage(11.5);
		public static readonly Voltage OperatingVoltage = new Voltage(12);
		public static readonly Voltage MaxVoltage = new Voltage(14.5);
	}
}
