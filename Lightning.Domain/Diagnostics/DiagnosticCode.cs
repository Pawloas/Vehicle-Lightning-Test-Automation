using System;
using System.Collections.Generic;
using System.Text;

namespace Lighting.Domain.Diagnostics
{
	public enum DiagnosticCode
	{
		None,
		InvalidLightMode,

		IntensityBellowTreshold,
		IntensityOverTreshold,

		HighTemperature,
		Overheating,

		UnderVoltage,
		OverVoltage
	}
}
