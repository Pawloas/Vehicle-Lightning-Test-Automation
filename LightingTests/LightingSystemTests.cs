using Lighting.Domain;
using Lighting.Domain.Diagnostics;

namespace LightingTests
{
	public class LightingSystemTests
	{
		[Fact]
		public void NewLightingSystem_ShouldBeOff()
		{
			LightingSystem lightingSystem = new LightingSystem();

			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}

		
		[Fact]
		public void NewLightingSystem_ShouldEnterPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem();

			lightingSystem.Voltage = LightingConstants.OperatingVoltage;
			lightingSystem.Temperature = LightingConstants.OperatingTemperature;
			lightingSystem.Intensity = LightingConstants.ParkingIntensity;

			lightingSystem.LightMode = LightMode.Position;

			Assert.Equal(LightMode.Position, lightingSystem.LightMode);
		}

	}
}
