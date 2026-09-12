using Lighting.Domain.Diagnostics.MeasurementsInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lighting.Domain.Diagnostics
{
	public class DiagnosticManager
	{
		public Diagnostic<Temperature> TemperatureDiagnostic { get; set; } = new Diagnostic<Temperature>(new Temperature(LightingConstants.OperatingTemperature));
		public Diagnostic<Voltage> VoltageDiagnostic { get; set; } = new Diagnostic<Voltage>(new Voltage(LightingConstants.OperatingVoltage));
		public Diagnostic<Intensity> IntensityDiagnostic { get; set; } = new Diagnostic<Intensity>(new Intensity(LightingConstants.StandardIntensity));

		private List<Diagnostic> AllDiagnostics => new List<Diagnostic> { TemperatureDiagnostic, VoltageDiagnostic, IntensityDiagnostic };


		public DiagnosticSeverity OverallSeverity()
		{
			return AllDiagnostics.Max(diagnostic => diagnostic.Severity);
		}

		public List<Diagnostic> OverallDiagnostics()
		{
			return AllDiagnostics;
		}
	
	}
}
