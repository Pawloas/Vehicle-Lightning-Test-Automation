using Lighting.Domain;
using Lighting.Domain.Diagnostics;

namespace LightingTests
{
	public class FogTests
	{
		[Fact]
		public void ShouldBeInFogMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.Fog);

			Assert.Equal(LightMode.Fog, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldEnterLowBeamMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.Fog);
			Assert.Equal(LightMode.Fog, lightingSystem.LightMode);

			lightingSystem.LightMode = LightMode.LowBeam;

			Assert.Equal(LightMode.LowBeam, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldNotEnterOffMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.Fog);
			Assert.Equal(LightMode.Fog, lightingSystem.LightMode);

			lightingSystem.LightMode = LightMode.Off;

			Assert.Equal(LightMode.Fog, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldNotEnterPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.Fog);
			Assert.Equal(LightMode.Fog, lightingSystem.LightMode);

			lightingSystem.LightMode = LightMode.Position;

			Assert.Equal(LightMode.Fog, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldNotEnterFogMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.Fog);
			Assert.Equal(LightMode.Fog, lightingSystem.LightMode);

			lightingSystem.LightMode = LightMode.Fog;

			Assert.Equal(LightMode.Fog, lightingSystem.LightMode);
		}
	}
}