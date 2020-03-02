using System;
using UnityEngine;

namespace FloatInstructions.ConcreteInstructions
{
	public class QuaternionInstruction : Instruction<Quaternion>
	{
		public QuaternionInstruction(Quaternion start, Quaternion finish,
			Func<float, float, float, float> easeFunction) :
			base(start, finish, easeFunction)
		{
		}

		protected override Quaternion _calculate(float time)
		{
			return Quaternion.Lerp(Start, Finish, EaseFunction(0, 1, time));
		}
	}
}