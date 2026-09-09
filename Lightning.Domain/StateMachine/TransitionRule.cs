using Lighting.Domain.Diagnostics;
using Lighting.Domain.Diagnostics.MeasurementsInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lighting.Domain.StateMachine
{
	public class TransitionRule
	{
		public LightMode NewTransitionMode { get; set; } = LightMode.Off;

		public AllowedRange<Voltage> VoltageRange { get; set; } = new AllowedRange<Voltage>(minRange: LightingConstants.MinVoltage, maxRange: LightingConstants.MaxVoltage);
		public AllowedRange<Temperature> TemperatureRange { get; set; } = new AllowedRange<Temperature>(minRange: LightingConstants.MinTemperature, maxRange: LightingConstants.MaxTemperature);
		public AllowedRange<Intensity> IntensityRange { get; set; } = new AllowedRange<Intensity>(minRange: LightingConstants.MinIntensity, maxRange: LightingConstants.MaxIntensity);
		
		public bool CanTransition(Voltage currentVoltage, Temperature currentTemperature, Intensity currentIntensity)
		{
			bool voltageInRange = VoltageRange.IsInRange(currentVoltage);
			bool temperatureInRange = TemperatureRange.IsInRange(currentTemperature);
			bool intensityInRange = IntensityRange.IsInRange(currentIntensity);

			return voltageInRange && temperatureInRange && intensityInRange;
		}

		public TransitionRule(LightMode newTransitionMode, AllowedRange<Voltage> voltageRange, AllowedRange<Temperature> temperatureRange, AllowedRange<Intensity> intensityRange)
		{
			NewTransitionMode = newTransitionMode;
			VoltageRange = voltageRange;
			TemperatureRange = temperatureRange;
			IntensityRange = intensityRange;
		}
	}
}
