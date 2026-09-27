using Lighting.Domain;
using Lighting.Domain.Diagnostics;

namespace LightingTests
{
	public class PositionTests
	{
		[Fact]
		public void ShouldBeInPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.Position);

			Assert.Equal(LightMode.Position, lightingSystem.LightMode);
		}

		[Fact]
		public void FromPositionToOffMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.Position);

			Assert.Equal(LightMode.Position, lightingSystem.LightMode);

			lightingSystem.LightMode = LightMode.Off;

			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}

		[Fact]
		public void FromPositionToLowBeamMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.LowBeam);

			Assert.Equal(LightMode.LowBeam, lightingSystem.LightMode);
		}


		[Fact]
		public void ShouldNotEnterHighBeamMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.Position);
			SetLightingSystemMode.SetMode(lightingSystem, LightMode.HighBeam);

			Assert.Equal(LightMode.Position, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldNotEnterFogMode()
		{
			LightingSystem lightingSystem = new LightingSystem();
			SetLightingSystemMode.PrepareAndSetMode(lightingSystem, LightMode.Position);
			SetLightingSystemMode.SetMode(lightingSystem, LightMode.Fog);

			Assert.Equal(LightMode.Position, lightingSystem.LightMode);
		}
	}
}
