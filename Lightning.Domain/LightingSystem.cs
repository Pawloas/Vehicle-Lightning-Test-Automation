using Lighting.Domain.Diagnostics;
using Lighting.Domain.Diagnostics.MeasurementsInfo;
using Lighting.Domain.StateMachine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Lighting.Domain
{
	public class LightingSystem
	{
		private readonly LightingStatus _lightingStatus = new LightingStatus();
		private readonly LightingStateMachine _lightingStateMachine = new LightingStateMachine();
		private readonly Diagnostic<Temperature> _temperature = new Diagnostic<Temperature>();

		public LightingSystem() { }


		public void SetLightMode(LightMode newLightMode)
		{
			LightMode currentLightMode = _lightingStatus.Mode;
			_lightingStatus.Mode = newLightMode;

			if (_lightingStateMachine.CanTransition(from: currentLightMode, 
													to: newLightMode, 
													currentVoltage: _lightingStatus.Voltage, 
													currentTemperature: _lightingStatus.Temperature, 
													currentIntensity: _lightingStatus.Intensity))
			{
				_lightingStatus.Diagnostic.SetDefaultSetting();
			}
			else
			{
				_lightingStatus.Diagnostic.SetDiagnosticSetting(code: DiagnosticCode.InvalidLightMode,
																severity: DiagnosticSeverity.Critical,
																parameter: DiagnosticParameter.LightMode,
																message: $"Transition from {currentLightMode} to {newLightMode} is not allowed !");
			}
		}

		public void SetIntensity(Intensity newIntensity) => _lightingStatus.Intensity = newIntensity;
		public void SetVoltage(Voltage newVoltage) => _lightingStatus.Voltage = newVoltage;
		public void SetTemperature(Temperature newTemperature) => _lightingStatus.Temperature = newTemperature;


		public LightMode GetLightMode() => _lightingStatus.Mode;
		public Intensity GetIntensity() => _lightingStatus.Intensity;
		public Voltage GetVoltage() => _lightingStatus.Voltage;
		public Temperature GetTemperature() => _lightingStatus.Temperature;
		public LightingStatus GetLightingStatus() => _lightingStatus;
	}
}
