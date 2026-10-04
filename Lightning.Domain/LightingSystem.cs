using Lighting.Domain.Diagnostics;
using Lighting.Domain.Diagnostics.MeasurementsInfo;
using Lighting.Domain.StateMachine;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace Lighting.Domain
{
	public class LightingSystem
	{
		private readonly LightingStatus _lightingStatus;

		public LightingSystem() 
		{ 
			_lightingStatus = new LightingStatus();
		}

		public bool TrySetLightMode(LightMode newMode) => _lightingStatus.TrySetLightMode(newMode);

		public LightMode LightMode
		{
			get => _lightingStatus.Mode;
			set => _lightingStatus.SetLightMode(value);
		}

		public Intensity Intensity
		{
			get => _lightingStatus.Intensity;
			set => _lightingStatus.Intensity = value;
		}

		public Voltage Voltage
		{
			get => _lightingStatus.Voltage;
			set => _lightingStatus.Voltage = value;
		}

		public Temperature Temperature
		{
			get => _lightingStatus.Temperature;
			set => _lightingStatus.Temperature = value;
		}

		public DiagnosticManager DiagnosticManager
		{
			get => _lightingStatus.DiagnosticManager;
		}

		public void SetMinVoltage()
		{
			Voltage = LightingConstants.MinVoltage;
		}
		public void SetMaxVoltage()
		{
			Voltage = LightingConstants.MaxVoltage;
		}

		public void SetMinTemperature()
		{
			Temperature = LightingConstants.MinTemperature;
		}

		public void SetMaxTemperature()
		{
			Temperature = LightingConstants.MaxTemperature;
		}

		public void SetMaxIntensity()
		{
			Intensity = LightingConstants.MaxIntensity;
		}
		public void SetMinIntensity()
		{
			Intensity = LightingConstants.MinIntensity;
		}

	}
}
