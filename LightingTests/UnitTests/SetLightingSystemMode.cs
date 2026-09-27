using Lighting.Domain;
using Lighting.Domain.Diagnostics;
using System;

namespace LightingTests
{
	public class SetLightingSystemMode
	{

		public static void SetMode(LightingSystem lightingSystem, LightMode targetMode)
		{
			switch (targetMode)
			{
				case LightMode.Off:
					SetLightingSystemToOffMode(lightingSystem);
					break;

				case LightMode.Position:
					SetLightingSystemToPositionMode(lightingSystem);
					break;

				case LightMode.LowBeam:
					SetLightingSystemToLowBeamMode(lightingSystem);
					break;

				case LightMode.HighBeam:
					SetLightingSystemToHighBeamMode(lightingSystem);
					break;

				case LightMode.Fog:
					SetLightingSystemToFogMode(lightingSystem);
					break;

				default:
					throw new ArgumentException($"Unsupported target mode: {targetMode}");
			}
		}

		public static void PrepareAndSetMode(LightingSystem lightingSystem, LightMode targetMode)
		{
			switch (targetMode)
			{
				case LightMode.Off:
					SetLightingSystemToOffMode(lightingSystem);
					break;

				case LightMode.Position:
					SetLightingSystemToOffMode(lightingSystem);
					SetLightingSystemToPositionMode(lightingSystem);
					break;

				case LightMode.LowBeam:
					SetLightingSystemToOffMode(lightingSystem);
					SetLightingSystemToPositionMode(lightingSystem);
					SetLightingSystemToLowBeamMode(lightingSystem);
					break;

				case LightMode.HighBeam:
					SetLightingSystemToOffMode(lightingSystem);
					SetLightingSystemToPositionMode(lightingSystem);
					SetLightingSystemToLowBeamMode(lightingSystem);
					SetLightingSystemToHighBeamMode(lightingSystem);
					break;

				case LightMode.Fog:
					SetLightingSystemToOffMode(lightingSystem);
					SetLightingSystemToPositionMode(lightingSystem);
					SetLightingSystemToLowBeamMode(lightingSystem);
					SetLightingSystemToFogMode(lightingSystem);
					break;

				default:
					throw new ArgumentException($"Unsupported target mode: {targetMode}");
			}
		}

		private static void SetLightingSystemToOffMode(LightingSystem lightingSystem)
		{
			lightingSystem.Voltage = LightingConstants.OperatingVoltage;
			lightingSystem.Temperature = LightingConstants.OperatingTemperature;
			lightingSystem.Intensity = LightingConstants.MinIntensity;
			lightingSystem.LightMode = LightMode.Off;
		}

		private static void SetLightingSystemToPositionMode(LightingSystem lightingSystem)
		{
			lightingSystem.Voltage = LightingConstants.OperatingVoltage;
			lightingSystem.Temperature = LightingConstants.OperatingTemperature;
			lightingSystem.Intensity = LightingConstants.ParkingIntensity;
			lightingSystem.LightMode = LightMode.Position;
		}

		private static void SetLightingSystemToLowBeamMode(LightingSystem lightingSystem)
		{
			lightingSystem.Voltage = LightingConstants.OperatingVoltage;
			lightingSystem.Temperature = LightingConstants.OperatingTemperature;
			lightingSystem.Intensity = LightingConstants.StandardIntensity;
			lightingSystem.LightMode = LightMode.LowBeam;
		}

		private static void SetLightingSystemToHighBeamMode(LightingSystem lightingSystem)
		{
			lightingSystem.Voltage = LightingConstants.OperatingVoltage;
			lightingSystem.Temperature = LightingConstants.OperatingTemperature;
			lightingSystem.Intensity = LightingConstants.StandardIntensity;
			lightingSystem.LightMode = LightMode.HighBeam;
		}

		private static void SetLightingSystemToFogMode(LightingSystem lightingSystem)
		{
			lightingSystem.Voltage = LightingConstants.OperatingVoltage;
			lightingSystem.Temperature = LightingConstants.OperatingTemperature;
			lightingSystem.Intensity = LightingConstants.StandardIntensity;
			lightingSystem.LightMode = LightMode.Fog;
		}

	}
}
