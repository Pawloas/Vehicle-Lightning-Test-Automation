using Lighting.Domain.Diagnostics.MeasurementsInfo;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Lighting.Domain.StateMachine;
using System.ComponentModel.DataAnnotations;

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

		public void SetMinIntensity()
		{
			Intensity = LightingConstants.MinIntensity;
		}
		public void SetMaxIntensity()
		{
			Intensity = LightingConstants.MaxIntensity;
		}
		public void SetMinTemperature()
		{
			Temperature = LightingConstants.MinTemperature;
		}
		public void SetMaxTemperature()
		{
			Temperature = LightingConstants.MaxTemperature;
		}
		public void SetMinVoltage()
		{
			Voltage = LightingConstants.MinVoltage;
		}
		public void SetMaxVoltage()
		{
			Voltage = LightingConstants.MaxVoltage;
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
					_intensityDiagnostic.SetDiagnosticSetting(
										code: DiagnosticCode.MinIntensity,
										severity: DiagnosticSeverity.Error,
										parameter: DiagnosticParameter.Intensity,
										message: $"Intensity value was set as ({value}), but permitted is <{LightingConstants.MinIntensity}, {LightingConstants.MaxIntensity}>");
				}

				else if (value > LightingConstants.MaxIntensity.Percentages)
				{
					_intensityDiagnostic.SetDiagnosticSetting(
										code: DiagnosticCode.MaxIntensity,
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

				if (value < LightingConstants.MinTemperature.Celsius)
				{
					_temperatureDiagnostic.SetDiagnosticSetting(
											code: DiagnosticCode.MinTemperature,
											severity: DiagnosticSeverity.Error,
											parameter: DiagnosticParameter.Temperature,
											message: $"Temperature value was set as ({value}), but it exceeded the threshold temperature which is ({LightingConstants.MinTemperature} °C)");
				}
				else if (value <= LightingConstants.MinWarningTemperature.Celsius)
				{
					_temperatureDiagnostic.SetDiagnosticSetting(
											code: DiagnosticCode.MinWarningTemperature,
											severity: DiagnosticSeverity.Warning,
											parameter: DiagnosticParameter.Temperature,
											message: $"Temperature value was set as ({value}), but it approached the threshold temperature which is ({LightingConstants.MinWarningTemperature} °C)");
					return;
				}
				else if (value >= LightingConstants.MaxWarningTemperature.Celsius)
				{
					_temperatureDiagnostic.SetDiagnosticSetting(
											code: DiagnosticCode.MaxWarningTemperature,
											severity: DiagnosticSeverity.Warning,
											parameter: DiagnosticParameter.Temperature,
											message: $"Temperature value was set as ({value}), but it approached the threshold temperature which is ({LightingConstants.MaxWarningTemperature} °C)");
					return;
				}
				else if (value > LightingConstants.MaxWarningTemperature.Celsius)
				{
					_temperatureDiagnostic.SetDiagnosticSetting(
											code: DiagnosticCode.MaxTemperature,
											severity: DiagnosticSeverity.Warning,
											parameter: DiagnosticParameter.Temperature,
											message: $"Temperature value was set as ({value}), but it exceeded the threshold temperature which is ({LightingConstants.MaxTemperature} °C)");
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

				if (value < LightingConstants.MinVoltage.Volts)
				{
					_voltageDiagnostic.SetDiagnosticSetting(
											code: DiagnosticCode.MinVoltage,
											severity: DiagnosticSeverity.Error,
											parameter: DiagnosticParameter.Voltage,
											message: $"Voltage value was set as ({value}), but it is below the under voltage threshold which is ({LightingConstants.MinVoltage} V)");
				}
				else if (value <= LightingConstants.MinWarningVoltage.Volts)
				{
					_voltageDiagnostic.SetDiagnosticSetting(
											code: DiagnosticCode.MinWarningVoltage,
											severity: DiagnosticSeverity.Warning,
											parameter: DiagnosticParameter.Voltage,
											message: $"Voltage value was set as ({value}), but it is below the warning voltage threshold which is ({LightingConstants.MinWarningVoltage} V)");
					return;
				}
				else if (value >= LightingConstants.MaxWarningVoltage.Volts)
				{
					_voltageDiagnostic.SetDiagnosticSetting(
											code: DiagnosticCode.MaxWarningVoltage,
											severity: DiagnosticSeverity.Warning,
											parameter: DiagnosticParameter.Voltage,
											message: $"Voltage value was set as ({value}), but it is above the warning voltage threshold which is ({LightingConstants.MaxWarningVoltage} V)");
					return;
				}
				else if (value > LightingConstants.MaxVoltage.Volts)
				{
					_voltageDiagnostic.SetDiagnosticSetting(
											code: DiagnosticCode.MaxVoltage,
											severity: DiagnosticSeverity.Error,
											parameter: DiagnosticParameter.Voltage,
											message: $"Voltage value was set as ({value}), but it exceeded the threshold voltage which is ({LightingConstants.MaxVoltage} V)");
				}
			}
		}

		public LightingStatus()
		{
			Mode = LightMode.Off;
			DiagnosticManager = new DiagnosticManager();
		}

	} 
}
