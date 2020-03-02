using System;
using UnityEngine;

namespace FloatInstructions.ConcreteInstructions
{
	public class ColorTweenInstruction : Instruction<Color>
	{
		protected override Color _calculate(float time)
		{
			return new Color(
				EaseFunction(Start.r, Finish.r, time),
				EaseFunction(Start.g, Finish.g, time),
				EaseFunction(Start.b, Finish.b, time),
				EaseFunction(Start.a, Finish.a, time));
		}

		public ColorTweenInstruction(Color start, Color finish, Func<float, float, float, float> easeFunction) :
			base(start, finish, easeFunction)
		{
		}
	}
}