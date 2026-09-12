using Lighting.Domain;
using Lighting.Domain.Diagnostics;

namespace LightingTests
{
	public class LightingSystemTests
	{
		[Fact]
		public void ShouldBeOff()
		{
			LightingSystem lightingSystem = new LightingSystem();

			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}

		[Fact]
		public void ShouldEnterPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem()
			{
				Voltage = LightingConstants.OperatingVoltage,
				Temperature = LightingConstants.OperatingTemperature,
				Intensity = LightingConstants.ParkingIntensity,

				LightMode = LightMode.Position
			};

			Assert.Equal(LightMode.Position, lightingSystem.LightMode);
		}

		[Fact]
		public void UnderVoltage_MustNotEnterPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem()
			{
				Voltage = LightingConstants.MinVoltage,
				Temperature = LightingConstants.OperatingTemperature,
				Intensity = LightingConstants.ParkingIntensity,
				LightMode = LightMode.Position
			};
			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}

		[Fact]
		public void OverVoltage_MustNotEnterPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem()
			{
				Voltage = LightingConstants.MaxVoltage,
				Temperature = LightingConstants.OperatingTemperature,
				Intensity = LightingConstants.ParkingIntensity,
				LightMode = LightMode.Position
			};

			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}

		[Fact]
		public void OverTemperature_MustNotEnterPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem()
			{
				Voltage = LightingConstants.OperatingVoltage,
				Temperature = LightingConstants.MaxTemperature,
				Intensity = LightingConstants.ParkingIntensity,
				LightMode = LightMode.Position
			};
			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}

		[Fact]
		public void OverIntensity_MustNotEnterPositionMode()
		{
			LightingSystem lightingSystem = new LightingSystem()
			{
				Voltage = LightingConstants.OperatingVoltage,
				Temperature = LightingConstants.OperatingTemperature,
				Intensity = LightingConstants.MaxIntensity,
				LightMode = LightMode.Position
			};
			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}

		[Fact]
		public void FromPositionToOffMode()
		{
			LightingSystem lightingSystem = new LightingSystem()
			{
				Voltage = LightingConstants.OperatingVoltage,
				Temperature = LightingConstants.OperatingTemperature,
				Intensity = LightingConstants.ParkingIntensity,
				LightMode = LightMode.Position
			};
			Assert.Equal(LightMode.Position, lightingSystem.LightMode);

			lightingSystem.Intensity = LightingConstants.MinIntensity;
			lightingSystem.LightMode = LightMode.Off;

			Assert.Equal(LightMode.Off, lightingSystem.LightMode);
		}



	}
}
