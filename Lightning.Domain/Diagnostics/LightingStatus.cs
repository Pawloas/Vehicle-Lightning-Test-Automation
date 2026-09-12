using Lighting.Domain.Diagnostics.MeasurementsInfo;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Lighting.Domain.StateMachine;

namespace Lighting.Domain.Diagnostics
{
	public class LightingStatus
	{

		public LightMode Mode { get; set; } = LightMode.Off;
		public DiagnosticManager DiagnosticManager { get; set; } = new DiagnosticManager();

		private Diagnostic<Intensity> _intensityDiagnostic => DiagnosticManager.IntensityDiagnostic;
		private Diagnostic<Temperature> _temperatureDiagnostic => DiagnosticManager.TemperatureDiagnostic;
		private Diagnostic<Voltage> _voltageDiagnostic => DiagnosticManager.VoltageDiagnostic;

		public void SetLightMode(LightMode newLightMode, bool canMakeTransition, HashSet<TransitionRule> transitionRules)
		{

			if (canMakeTransition == false)
			{
				Console.WriteLine($"Transition from {Mode} to {newLightMode} is not allowed !");
				return;
			}

			TransitionRule transitionRule = transitionRules.First();

			bool areCurrentMeasurementsSeverityNotCritical = _intensityDiagnostic.Severity == DiagnosticSeverity.Info &&
															 _temperatureDiagnostic.Severity == DiagnosticSeverity.Info &&
															 _voltageDiagnostic.Severity == DiagnosticSeverity.Info;

			bool areNewMeasurementsInRange = transitionRule.AreMeasurementsInRange(currentVoltage: _voltageDiagnostic.ActualValue,
																					currentTemperature: _temperatureDiagnostic.ActualValue,
																					currentIntensity: _intensityDiagnostic.ActualValue);

			if (areCurrentMeasurementsSeverityNotCritical && areNewMeasurementsInRange)
			{
				Mode = newLightMode;
				_intensityDiagnostic.SetDefaultSetting();
				_temperatureDiagnostic.SetDefaultSetting();
				_voltageDiagnostic.SetDefaultSetting();
			}

		}

		public Intensity Intensity
		{
			get => _intensityDiagnostic.ActualValue;
			set
			{
				_intensityDiagnostic.ActualValue = value;
				_intensityDiagnostic.SetDefaultSetting();

				if (value < LightingConstants.MinIntensity.Percentages)
				{
					_intensityDiagnostic.SetDiagnosticSetting(code: DiagnosticCode.IntensityBellowTreshold,
											severity: DiagnosticSeverity.Error,
											parameter: DiagnosticParameter.Intensity,
											message: $"Intensity value was set as ({value}), but permitted is <{LightingConstants.MinIntensity}, {LightingConstants.MaxIntensity}>");
				}

				else if (value > LightingConstants.MaxIntensity.Percentages)
				{
					_intensityDiagnostic.SetDiagnosticSetting(code: DiagnosticCode.IntensityOverTreshold,
										severity: DiagnosticSeverity.Error,
										parameter: DiagnosticParameter.Intensity,
										message: $"Intensity value was set as ({value}), but permitted is <{LightingConstants.MinIntensity}, {LightingConstants.MaxIntensity}>");
				}
			}
		}

		public Temperature Temperature
		{
			get => _temperatureDiagnostic.ActualValue;
			set
			{
				_temperatureDiagnostic.ActualValue = value;
				_temperatureDiagnostic.SetDefaultSetting();

				if (value >= LightingConstants.MaxTemperature.Celsius)
				{
					_temperatureDiagnostic.SetDiagnosticSetting(code: DiagnosticCode.Overheating,
											severity: DiagnosticSeverity.Critical,
											parameter: DiagnosticParameter.Temperature,
											message: $"Temperature value was set as ({value}), but it exceeded the threshold temperature which is ({LightingConstants.MaxTemperature} °C)");
				}

				else if (value >= LightingConstants.HighTresholdTemperature.Celsius)
				{
					_temperatureDiagnostic.SetDiagnosticSetting(code: DiagnosticCode.HighTemperature,
											severity: DiagnosticSeverity.Warning,
											parameter: DiagnosticParameter.Temperature,
											message: $"Temperature value was set as ({value}), but it approached the threshold temperature which is ({LightingConstants.HighTresholdTemperature} °C)");
				}
			}
		}
		public Voltage Voltage
		{
			get => _voltageDiagnostic.ActualValue;
			set
			{
				_voltageDiagnostic.ActualValue = value;
				_voltageDiagnostic.SetDefaultSetting();

				if (value <= LightingConstants.CriticalLowVoltage.Volts)
				{
					_voltageDiagnostic.SetDiagnosticSetting(code: DiagnosticCode.UnderVoltage,
										severity: DiagnosticSeverity.Warning,
										parameter: DiagnosticParameter.Voltage,
										message: $"Voltage value was set as ({value}), but it is bellow under voltage treshold, which is ({LightingConstants.CriticalLowVoltage} V)");
				}
				else if (value <= LightingConstants.MinVoltage.Volts)
				{
					_voltageDiagnostic.SetDiagnosticSetting(code: DiagnosticCode.UnderVoltage,
										severity: DiagnosticSeverity.Critical,
										parameter: DiagnosticParameter.Voltage,
										message: $"Voltage value was set as ({value}), but it approached voltage treshold, which is ({LightingConstants.MinVoltage} V)");
				}
				else if (value >= LightingConstants.MaxVoltage.Volts)
				{
					_voltageDiagnostic.SetDiagnosticSetting(code: DiagnosticCode.OverVoltage,
										severity: DiagnosticSeverity.Critical,
										parameter: DiagnosticParameter.Voltage,
										message: $"Voltage value was set as ({value}), but it exceeded voltage treshold, which is ({LightingConstants.MaxVoltage} V)");
				}
			}
		}


		public LightingStatus()
		{
			//Mode = LightMode.Off;
			//DiagnosticManager = new DiagnosticManager();
		}
		
	} 
}
