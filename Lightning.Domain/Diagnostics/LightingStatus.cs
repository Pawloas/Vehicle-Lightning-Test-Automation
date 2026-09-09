using Lighting.Domain.Diagnostics.MeasurementsInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lighting.Domain.Diagnostics
{
	public class LightingStatus
	{

		public LightMode Mode { get; set; } = LightMode.Off;
		public Diagnostic<Intensity> DiagnosticIntensity { get; set; } = new Diagnostic<Intensity>();
		public Diagnostic<Temperature> DiagnosticTemperature { get; set; } = new Diagnostic<Temperature>();
		public Diagnostic<Voltage> DiagnosticVoltage { get; set; } = new Diagnostic<Voltage>();

		private Intensity _intensite = new Intensity(0);
		private Temperature _temperature = new Temperature(0);
		private Voltage _voltage = new Voltage(0);

		public Intensity Intensity
		{
			get => _intensite;
			set
			{
				_intensite = value;
				DiagnosticIntensity.SetDefaultSetting();

				if (value < LightingConstants.MinIntensity.Percentages)
				{
					DiagnosticIntensity.SetDiagnosticSetting(code: DiagnosticCode.IntensityBellowTreshold,
											severity: DiagnosticSeverity.Error,
											parameter: DiagnosticParameter.Intensity,
											message: $"Intensity value was set as ({value}), but permitted is <{LightingConstants.MinIntensity}, {LightingConstants.MaxIntensity}>");
				}

				else if (value > LightingConstants.MaxIntensity.Percentages)
				{
					DiagnosticIntensity.SetDiagnosticSetting(code: DiagnosticCode.IntensityOverTreshold,
										severity: DiagnosticSeverity.Error,
										parameter: DiagnosticParameter.Intensity,
										message: $"Intensity value was set as ({value}), but permitted is <{LightingConstants.MinIntensity}, {LightingConstants.MaxIntensity}>");
				}

		}
		public Temperature Temperature
		{
			get => _temperature;
			set
			{
				_temperature = value;
				DiagnosticTemperature.SetDefaultSetting();

				if (value >= LightingConstants.MaxTemperature.Celsius)
				{
					DiagnosticTemperature.SetDiagnosticSetting(code: DiagnosticCode.Overheating,
											severity: DiagnosticSeverity.Critical,
											parameter: DiagnosticParameter.Temperature,
											message: $"Temperature value was set as ({value}), but it exceeded the threshold temperature which is ({LightingConstants.MaxTemperature} °C)");
				}

				else if (value >= LightingConstants.HighTresholdTemperature.Celsius)
				{
					DiagnosticTemperature.SetDiagnosticSetting(code: DiagnosticCode.HighTemperature,
											severity: DiagnosticSeverity.Warning,
											parameter: DiagnosticParameter.Temperature,
											message: $"Temperature value was set as ({value}), but it approached the threshold temperature which is ({LightingConstants.HighTresholdTemperature} °C)");
					}

			}
		}
		public Voltage Voltage
		{
			get => _voltage;
			set
			{
				_voltage = value;
				DiagnosticVoltage.SetDefaultSetting();

				if (value <= LightingConstants.CriticalLowVoltage.Volts)
				{
					DiagnosticVoltage.SetDiagnosticSetting(code: DiagnosticCode.UnderVoltage,
										severity: DiagnosticSeverity.Warning,
										parameter: DiagnosticParameter.Voltage,
										message: $"Voltage value was set as ({value}), but it is bellow under voltage treshold, which is ({LightingConstants.CriticalLowVoltage} V)");
				}
				else if (value <= LightingConstants.MinVoltage.Volts)
				{
					DiagnosticVoltage.SetDiagnosticSetting(code: DiagnosticCode.UnderVoltage,
										severity: DiagnosticSeverity.Critical,
										parameter: DiagnosticParameter.Voltage,
										message: $"Voltage value was set as ({value}), but it approached voltage treshold, which is ({LightingConstants.MinVoltage} V)");
				}
				else if (value >= LightingConstants.MaxVoltage.Volts)
				{
					DiagnosticVoltage.SetDiagnosticSetting(code: DiagnosticCode.OverVoltage,
										severity: DiagnosticSeverity.Critical,
										parameter: DiagnosticParameter.Voltage,
										message: $"Voltage value was set as ({value}), but it exceeded voltage treshold, which is ({LightingConstants.MaxVoltage} V)");
				}


		}


		public LightingStatus()
		{
			Mode = LightMode.Off;
				Intensity = LightingConstants.MinIntensity;
				Temperature = LightingConstants.OperatingTemperature;
				Voltage = LightingConstants.OperatingVoltage;
				DiagnosticIntensity = new Diagnostic<Intensity>();
				DiagnosticTemperature = new Diagnostic<Temperature>();
				DiagnosticVoltage = new Diagnostic<Voltage>();
			}
		
	} 
}
