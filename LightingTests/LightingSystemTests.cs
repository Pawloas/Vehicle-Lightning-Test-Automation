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
			LightingStatus lightingStatus = lightingSystem.GetLightingStatus();

			Assert.Equal(LightMode.Off, lightingStatus.Mode);
		}

		
		[Fact]
		public void NewLightingSystem_ShouldEnterPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			LightingStatus lightingStatus = lightingSystem.GetLightingStatus();

			lightingSystem.SetVoltage(LightingConstants.OperatingVoltage);
			lightingSystem.SetTemperature(LightingConstants.OperatingTemperature);
			lightingSystem.SetIntensity(LightingConstants.ParkingIntensity);

			lightingSystem.SetLightMode(LightMode.Position);

			Assert.Equal(LightMode.Position, lightingStatus.Mode);
		}

	}
}
