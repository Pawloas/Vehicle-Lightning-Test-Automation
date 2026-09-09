using Lighting.Domain.Diagnostics;
using Lighting.Domain.Diagnostics.MeasurementsInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lighting.Domain.StateMachine
{
	public class LightingStateMachine
	{
		/*private readonly Dictionary<LightMode, HashSet<LightMode>> _transtionRules = new Dictionary<LightMode, HashSet<LightMode>>
		{
			[LightMode.Off] = new() { LightMode.Position },
			[LightMode.Position] = new() { LightMode.LowBeam, LightMode.Off },
			[LightMode.LowBeam] = new() { LightMode.HighBeam, LightMode.Fog, LightMode.Off },
			[LightMode.HighBeam] = new() { LightMode.LowBeam },
			[LightMode.Fog] = new() { LightMode.LowBeam }
		};*/

		private readonly Dictionary<LightMode, HashSet<TransitionRule>> _transtionRules = new Dictionary<LightMode, HashSet<TransitionRule>>
		{
			[LightMode.Off] = new() 
				{ 
					new TransitionRule(newTransitionMode: LightMode.Position,
									   voltageRange: new AllowedRange<Voltage> (minRange: LightingConstants.CriticalLowVoltage,
														 					   maxRange: LightingConstants.MaxVoltage),

									   temperatureRange: new AllowedRange<Temperature> (minRange: LightingConstants.MinTemperature,
																			       maxRange: LightingConstants.HighTresholdTemperature),

									   intensityRange: new AllowedRange<Intensity> (minRange: LightingConstants.MinIntensity,
																		      maxRange: LightingConstants.ReducedIntensity))
				},

			[LightMode.Position] = new()
				{ 
					new TransitionRule(newTransitionMode: LightMode.LowBeam,
									   voltageRange: new AllowedRange<Voltage> (minRange: LightingConstants.MinVoltage,
																			   maxRange: LightingConstants.MaxVoltage),

									   temperatureRange: new AllowedRange<Temperature> (minRange: LightingConstants.MinTemperature,
																			       maxRange: LightingConstants.HighTresholdTemperature),

									   intensityRange: new AllowedRange<Intensity> (minRange: LightingConstants.ParkingIntensity,
																		      maxRange: LightingConstants.MaxIntensity)),

					new TransitionRule(newTransitionMode: LightMode.Off,
									   voltageRange: new AllowedRange<Voltage> (minRange: LightingConstants.CriticalLowVoltage,
																			   maxRange: LightingConstants.MaxVoltage),

									   temperatureRange: new AllowedRange<Temperature> (minRange: LightingConstants.MinTemperature,
																			       maxRange: LightingConstants.MaxTemperature),

									   intensityRange: new AllowedRange<Intensity> (minRange: LightingConstants.MinIntensity,
																			  maxRange: LightingConstants.MaxIntensity))
				},
			[LightMode.LowBeam] = new()
				{
						new TransitionRule(newTransitionMode: LightMode.HighBeam,
										   voltageRange: new AllowedRange<Voltage> (minRange: LightingConstants.MinStableVoltage,
																				   maxRange: LightingConstants.MaxVoltage),

										   temperatureRange: new AllowedRange<Temperature> (minRange: LightingConstants.MinTemperature,
																					   maxRange: LightingConstants.WarningTemperature),

										   intensityRange: new AllowedRange<Intensity> (minRange: LightingConstants.StandardIntensity,
																			      maxRange: LightingConstants.MaxIntensity)),

						new TransitionRule(newTransitionMode: LightMode.Fog,
										   voltageRange: new AllowedRange<Voltage> (minRange: LightingConstants.MinVoltage,
																				   maxRange: LightingConstants.MaxVoltage),

										   temperatureRange: new AllowedRange<Temperature> (minRange: LightingConstants.MinTemperature,
																					   maxRange: LightingConstants.HighTresholdTemperature),

										   intensityRange: new AllowedRange<Intensity> (minRange: LightingConstants.ParkingIntensity,
																				  maxRange: LightingConstants.MaxIntensity)),

						new TransitionRule(newTransitionMode: LightMode.Off,
										   voltageRange: new AllowedRange<Voltage> (minRange: LightingConstants.CriticalLowVoltage,
																				   maxRange: LightingConstants.MaxVoltage),

										   temperatureRange: new AllowedRange<Temperature> (minRange: LightingConstants.MinTemperature,
																					maxRange: LightingConstants.MaxTemperature),

										   intensityRange: new AllowedRange<Intensity> (minRange: LightingConstants.MinIntensity,
																				  maxRange: LightingConstants.MaxIntensity)),
				},

			[LightMode.HighBeam] = new()
			{
				new TransitionRule(newTransitionMode: LightMode.LowBeam,
								   voltageRange: new AllowedRange<Voltage> (minRange: LightingConstants.CriticalLowVoltage,
																		   maxRange: LightingConstants.MaxVoltage),

								   temperatureRange: new AllowedRange<Temperature> (minRange: LightingConstants.MinTemperature,
																			   maxRange: LightingConstants.HighTresholdTemperature),

								   intensityRange: new AllowedRange<Intensity> (minRange: LightingConstants.ParkingIntensity,
																		  maxRange: LightingConstants.MaxIntensity)),
			},

			[LightMode.Fog] = new() 
				{
					new TransitionRule(newTransitionMode: LightMode.LowBeam,
									   voltageRange: new AllowedRange<Voltage> (minRange: LightingConstants.CriticalLowVoltage,
																			   maxRange: LightingConstants.MaxVoltage),

									   temperatureRange: new AllowedRange<Temperature> (minRange: LightingConstants.MinTemperature,
																			    maxRange: LightingConstants.HighTresholdTemperature),

									   intensityRange: new AllowedRange<Intensity> (minRange: LightingConstants.ParkingIntensity,
																			  maxRange: LightingConstants.MaxIntensity)),
				}
		};

		public bool CanTransition(LightMode from, LightMode to, Voltage currentVoltage, Temperature currentTemperature, Intensity currentIntensity)
		{
			bool canPossiblyTransitionToMode = _transtionRules.TryGetValue(from, out HashSet<TransitionRule>? transtionRulesResult);

			if (!canPossiblyTransitionToMode || (transtionRulesResult == null))
				return false;

			return transtionRulesResult.Any(transition => transition.NewTransitionMode == to && transition.CanTransition(currentVoltage, currentTemperature, currentIntensity));
		}

		public LightingStateMachine() { }
	}
}
