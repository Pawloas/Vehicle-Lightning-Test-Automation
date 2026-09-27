using Lighting.Domain;
using Lighting.Domain.Diagnostics;

namespace LightingTests
{
	public class OffModeTests
	{
		[Fact]
		public void ShouldBeInOffMode()
		{
			LightingSystem lightingSystem = new LightingSystem();

			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldEnterPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.SetMode(lightingSystem, LightMode.Position);

			Assert.Equal(LightMode.Position, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldNotEnterLowBeamMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.SetMode(lightingSystem, LightMode.LowBeam);

			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}


		[Fact]
		public void ShouldNotEnterHighBeamMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.SetMode(lightingSystem, LightMode.HighBeam);

			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldNotEnterFogMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.SetMode(lightingSystem, LightMode.Fog);

			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}


		[Fact]
		public void UnderVoltage_MustNotEnterPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			lightingSystem.SetMinVoltage();

			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}

		[Fact]
		public void OverVoltage_MustNotEnterPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			lightingSystem.SetMaxVoltage();

			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}

		[Fact]
		public void UnderTemperature_MustNotEnterPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			lightingSystem.SetMinTemperature();

			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}

		[Fact]
		public void OverTemperature_MustNotEnterPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			lightingSystem.SetMaxTemperature();

			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}

		[Fact]
		public void OverIntensity_MustNotEnterPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			lightingSystem.SetMaxIntensity();

			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}
	}
}
