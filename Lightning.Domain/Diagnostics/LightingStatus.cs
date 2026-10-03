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
		public DiagnosticManager DiagnosticManager { get; set; }

		private Diagnostic<Intensity> IntensityDiagnostic => DiagnosticManager.IntensityDiagnostic;
		private Diagnostic<Temperature> TemperatureDiagnostic => DiagnosticManager.TemperatureDiagnostic;
		private Diagnostic<Voltage> VoltageDiagnostic => DiagnosticManager.VoltageDiagnostic;

		public void SetLightMode(LightMode newLightMode, bool canMakeTransition, HashSet<TransitionRule> transitionRules)
		{

			if (canMakeTransition == false)
			{
				Console.WriteLine($"Transition from {Mode} to {newLightMode} is not allowed !");
				return;
			}

			TransitionRule transitionRule = transitionRules.First();

			bool areCurrentMeasurementsSeverityNotCritical = IntensityDiagnostic.Severity == DiagnosticSeverity.Info &&
															 TemperatureDiagnostic.Severity == DiagnosticSeverity.Info &&
															 VoltageDiagnostic.Severity == DiagnosticSeverity.Info;

			bool areNewMeasurementsInRange = transitionRule.AreMeasurementsInRange(currentVoltage: VoltageDiagnostic.ActualValue,
																					currentTemperature: TemperatureDiagnostic.ActualValue,
																					currentIntensity: IntensityDiagnostic.ActualValue);

			if (areCurrentMeasurementsSeverityNotCritical && areNewMeasurementsInRange)
			{
				Mode = newLightMode;
				IntensityDiagnostic.SetDefaultSetting();
				TemperatureDiagnostic.SetDefaultSetting();
				VoltageDiagnostic.SetDefaultSetting();
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
			get => IntensityDiagnostic.ActualValue;
			set
			{
				IntensityDiagnostic.ActualValue = value;
				IntensityDiagnostic.SetDefaultSetting();

				if (value < LightingConstants.MinIntensity.Percentages)
				{
					IntensityDiagnostic.SetDiagnosticSetting(
										code: DiagnosticCode.MinIntensity,
										severity: DiagnosticSeverity.Error,
										message: $"Intensity value was set as ({value}), but permitted is <{LightingConstants.MinIntensity}, {LightingConstants.MaxIntensity}>");
				}

				else if (value > LightingConstants.MaxIntensity.Percentages)
				{
					IntensityDiagnostic.SetDiagnosticSetting(
										code: DiagnosticCode.MaxIntensity,
										severity: DiagnosticSeverity.Error,
										message: $"Intensity value was set as ({value}), but permitted is <{LightingConstants.MinIntensity}, {LightingConstants.MaxIntensity}>");
				}
			}
		}

		public Temperature Temperature
		{
			get => TemperatureDiagnostic.ActualValue;
			set
			{
				TemperatureDiagnostic.ActualValue = value;
				TemperatureDiagnostic.SetDefaultSetting();

				if (value < LightingConstants.MinTemperature.Celsius)
				{
					TemperatureDiagnostic.SetDiagnosticSetting(
											code: DiagnosticCode.MinTemperature,
											severity: DiagnosticSeverity.Error,
											message: $"Temperature value was set as ({value}), but it exceeded the threshold temperature which is ({LightingConstants.MinTemperature} °C)");
				}
				else if (value <= LightingConstants.MinWarningTemperature.Celsius)
				{
					TemperatureDiagnostic.SetDiagnosticSetting(
											code: DiagnosticCode.MinWarningTemperature,
											severity: DiagnosticSeverity.Warning,
											message: $"Temperature value was set as ({value}), but it approached the threshold temperature which is ({LightingConstants.MinWarningTemperature} °C)");
					return;
				}
				else if (value >= LightingConstants.MaxWarningTemperature.Celsius)
				{
					TemperatureDiagnostic.SetDiagnosticSetting(
											code: DiagnosticCode.MaxWarningTemperature,
											severity: DiagnosticSeverity.Warning,
											message: $"Temperature value was set as ({value}), but it approached the threshold temperature which is ({LightingConstants.MaxWarningTemperature} °C)");
					return;
				}
				else if (value > LightingConstants.MaxWarningTemperature.Celsius)
				{
					TemperatureDiagnostic.SetDiagnosticSetting(
											code: DiagnosticCode.MaxTemperature,
											severity: DiagnosticSeverity.Warning,
											message: $"Temperature value was set as ({value}), but it exceeded the threshold temperature which is ({LightingConstants.MaxTemperature} °C)");
				}
			}
		}

		public Voltage Voltage
		{
			get => VoltageDiagnostic.ActualValue;
			set
			{
				VoltageDiagnostic.ActualValue = value;
				VoltageDiagnostic.SetDefaultSetting();

				if (value < LightingConstants.MinVoltage.Volts)
				{
					VoltageDiagnostic.SetDiagnosticSetting(
											code: DiagnosticCode.MinVoltage,
											severity: DiagnosticSeverity.Error,
											message: $"Voltage value was set as ({value}), but it is below the under voltage threshold which is ({LightingConstants.MinVoltage} V)");
				}
				else if (value <= LightingConstants.MinWarningVoltage.Volts)
				{
					VoltageDiagnostic.SetDiagnosticSetting(
											code: DiagnosticCode.MinWarningVoltage,
											severity: DiagnosticSeverity.Warning,
											message: $"Voltage value was set as ({value}), but it is below the warning voltage threshold which is ({LightingConstants.MinWarningVoltage} V)");
					return;
				}
				else if (value >= LightingConstants.MaxWarningVoltage.Volts)
				{
					VoltageDiagnostic.SetDiagnosticSetting(
											code: DiagnosticCode.MaxWarningVoltage,
											severity: DiagnosticSeverity.Warning,
											message: $"Voltage value was set as ({value}), but it is above the warning voltage threshold which is ({LightingConstants.MaxWarningVoltage} V)");
					return;
				}
				else if (value > LightingConstants.MaxVoltage.Volts)
				{
					VoltageDiagnostic.SetDiagnosticSetting(
											code: DiagnosticCode.MaxVoltage,
											severity: DiagnosticSeverity.Error,
											message: $"Voltage value was set as ({value}), but it exceeded the threshold voltage which is ({LightingConstants.MaxVoltage} V)");
				}
			}
		}
		public LightingStatus()
		{
			Mode = LightMode.Off;
			DiagnosticManager = new DiagnosticManager(
					temperatureDiagnostic: new Diagnostic<Temperature>(new Temperature(LightingConstants.OperatingTemperature)),
					voltageDiagnostic: new Diagnostic<Voltage>(new Voltage(LightingConstants.OperatingVoltage)),
					intensityDiagnostic: new Diagnostic<Intensity>(new Intensity(LightingConstants.StandardIntensity))
			);

		}

	} 
}
