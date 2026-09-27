using Lighting.Domain;
using Lighting.Domain.Diagnostics;

namespace LightingTests
{
	public class MeasurementsTests
	{
		[Theory]
		[InlineData(LightMode.Off)]
		[InlineData(LightMode.Position)]
		[InlineData(LightMode.LowBeam)]
		[InlineData(LightMode.HighBeam)]
		[InlineData(LightMode.Fog)]
		public void CannotChangeLightMode_While_UnderVoltage_Tests(LightMode currentMode)
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, currentMode);
			lightingSystem.SetMinVoltage();

			Assert.Equal(currentMode, lightingSystem.LightMode);
		}

		[Theory]
		[InlineData(LightMode.Off)]
		[InlineData(LightMode.Position)]
		[InlineData(LightMode.LowBeam)]
		[InlineData(LightMode.HighBeam)]
		[InlineData(LightMode.Fog)]
		public void CannotChangeLightMode_While_OverVoltage_Tests(LightMode currentMode)
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, currentMode);
			lightingSystem.SetMaxVoltage();

			Assert.Equal(currentMode, lightingSystem.LightMode);
		}

		[Theory]
		[InlineData(LightMode.Off)]
		[InlineData(LightMode.Position)]
		[InlineData(LightMode.LowBeam)]
		[InlineData(LightMode.HighBeam)]
		[InlineData(LightMode.Fog)]
		public void CannotChangeLightMode_While_LowTemperature_Tests(LightMode currentMode)
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, currentMode);
			lightingSystem.SetMinTemperature();

			Assert.Equal(currentMode, lightingSystem.LightMode);
		}

		[Theory]
		[InlineData(LightMode.Off)]
		[InlineData(LightMode.Position)]
		[InlineData(LightMode.LowBeam)]
		[InlineData(LightMode.HighBeam)]
		[InlineData(LightMode.Fog)]
		public void CannotChangeLightMode_While_HighTemperature_Tests(LightMode currentMode)
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, currentMode);
			lightingSystem.SetMaxTemperature();

			Assert.Equal(currentMode, lightingSystem.LightMode);
		}

		[Theory]
		[InlineData(LightMode.Off)]
		[InlineData(LightMode.Position)]
		[InlineData(LightMode.LowBeam)]
		[InlineData(LightMode.HighBeam)]
		[InlineData(LightMode.Fog)]
		public void CannotChangeLightMode_While_LowIntensity_Tests(LightMode currentMode)
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, currentMode);
			lightingSystem.SetMinIntensity();

			Assert.Equal(currentMode, lightingSystem.LightMode);
		}

		[Theory]
		[InlineData(LightMode.Off)]
		[InlineData(LightMode.Position)]
		[InlineData(LightMode.LowBeam)]
		[InlineData(LightMode.HighBeam)]
		[InlineData(LightMode.Fog)]
		public void CannotChangeLightMode_While_HighIntensity_Tests(LightMode currentMode)
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, currentMode);
			lightingSystem.SetMaxIntensity();

			Assert.Equal(currentMode, lightingSystem.LightMode);
		}
	}
}
