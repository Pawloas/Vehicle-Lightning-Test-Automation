using Lighting.Domain.Diagnostics;
using Lighting.Domain.Diagnostics.MeasurementsInfo;
using Lighting.Domain.StateMachine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Lighting.Domain
{
	public class LightingSystem
	{
		private readonly LightingStatus _lightingStatus = new LightingStatus();
		private readonly LightingStateMachine _lightingStateMachine = new LightingStateMachine();

		public LightingSystem() { }

		public LightMode LightMode
		{
			get => _lightingStatus.Mode;
			set
			{
				HashSet<TransitionRule> transitionRules = _lightingStateMachine.TransitionRulesToNewMode(from: _lightingStatus.Mode, to: value);
				bool canMakeTransition = _lightingStateMachine.DoesTransitionRuleExist(transitionRules);

				_lightingStatus.SetLightMode(value, _lightingStateMachine.CanMakeTransitionToNewMode(from: _lightingStatus.Mode, to: value), transitionRules);
			}
		}

		public Intensity Intensity
		{
			get => _lightingStatus.Intensity;
			set => _lightingStatus.Intensity = value;
		}

		public Voltage Voltage
		{
			get => _lightingStatus.Voltage;
			set => _lightingStatus.Voltage = value;
		}

		public Temperature Temperature
		{
			get => _lightingStatus.Temperature;
			set => _lightingStatus.Temperature = value;
		}

	}
}
