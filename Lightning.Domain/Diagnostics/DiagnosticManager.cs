using Lighting.Domain.Diagnostics.MeasurementsInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lighting.Domain.Diagnostics
{
	public class DiagnosticManager
	{
		public DiagnosticCode OverAllCode { get; set; } = DiagnosticCode.None;
		public DiagnosticSeverity OverAllSeverity { get; set; } = DiagnosticSeverity.Info;
		public Diagnostic<Temperature> TemperatureDiagnostic { get; set; } = new Diagnostic<Temperature>();
		public Diagnostic<Voltage> VoltageDiagnostic { get; set; } = new Diagnostic<Voltage>();
		public Diagnostic<Intensity> IntensityDiagnostic { get; set; } = new Diagnostic<Intensity>();
	}
}
