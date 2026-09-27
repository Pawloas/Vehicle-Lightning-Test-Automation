using Lighting.Domain;
using Lighting.Domain.Diagnostics;

namespace LightingTests
{
	public class LowBeamTests
	{
		[Fact]
		public void ShouldBeInLowBeamMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.LowBeam);

			Assert.Equal(LightMode.LowBeam, lightingSystem.LightMode);
		}

		[Fact]
		public void FromLowBeamToOffMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.LowBeam);
			Assert.Equal(LightMode.LowBeam, lightingSystem.LightMode);

			lightingSystem.LightMode = LightMode.Off;

			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldEnterHighBeamMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.LowBeam);
			Assert.Equal(LightMode.LowBeam, lightingSystem.LightMode);

			lightingSystem.LightMode = LightMode.HighBeam;

			Assert.Equal(LightMode.HighBeam, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldEnterFogMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.LowBeam);
			Assert.Equal(LightMode.LowBeam, lightingSystem.LightMode);

			lightingSystem.LightMode = LightMode.Fog;

			Assert.Equal(LightMode.Fog, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldEnterOffMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.LowBeam);
			Assert.Equal(LightMode.LowBeam, lightingSystem.LightMode);

			lightingSystem.LightMode = LightMode.Off;

			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldNotEnterPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.LowBeam);
			Assert.Equal(LightMode.LowBeam, lightingSystem.LightMode);

			lightingSystem.LightMode = LightMode.Position;

			Assert.Equal(LightMode.LowBeam, lightingSystem.LightMode);
		}
	}
}