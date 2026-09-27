using Lighting.Domain;
using Lighting.Domain.Diagnostics;

namespace LightingTests
{
	public class HighBeamTests
	{
		[Fact]
		public void ShouldBeInHighBeamMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.HighBeam);

			Assert.Equal(LightMode.HighBeam, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldEnterLowBeamMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.HighBeam);
			Assert.Equal(LightMode.HighBeam, lightingSystem.LightMode);

			lightingSystem.LightMode = LightMode.LowBeam;

			Assert.Equal(LightMode.LowBeam, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldNotEnterOffMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.HighBeam);
			Assert.Equal(LightMode.HighBeam, lightingSystem.LightMode);

			lightingSystem.LightMode = LightMode.Off;

			Assert.Equal(LightMode.HighBeam, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldNotEnterPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.HighBeam);
			Assert.Equal(LightMode.HighBeam, lightingSystem.LightMode);

			lightingSystem.LightMode = LightMode.Position;

			Assert.Equal(LightMode.HighBeam, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldNotEnterFogMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.HighBeam);
			Assert.Equal(LightMode.HighBeam, lightingSystem.LightMode);

			lightingSystem.LightMode = LightMode.Fog;

			Assert.Equal(LightMode.HighBeam, lightingSystem.LightMode);
		}
	}
}