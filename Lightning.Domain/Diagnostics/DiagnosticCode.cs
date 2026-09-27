using System;
using System.Collections.Generic;
using System.Text;

namespace Lighting.Domain.Diagnostics
{
	public enum DiagnosticCode
	{
		None,
		InvalidLightMode,

		MinIntensity,
		MaxIntensity,

		MinTemperature,
		MinWarningTemperature,
		MaxWarningTemperature,
		MaxTemperature,

		MinVoltage,
		MinWarningVoltage,
		MaxWarningVoltage,
		MaxVoltage,
	}
}
