using System;
using UnityEngine;

namespace FloatInstructions.ConcreteInstructions
{
	public class Vector3TweenInstruction : Instruction<Vector3>
	{
		protected override Vector3 _calculate(float time)
		{
			return new Vector3(
				Mathf.Abs(Finish.x - Start.x) > Precision ? EaseFunction(Start.x, Finish.x, time) : Finish.x,
				Mathf.Abs(Finish.y - Start.y) > Precision ? EaseFunction(Start.y, Finish.y, time) : Finish.y,
				Mathf.Abs(Finish.z - Start.z) > Precision ? EaseFunction(Start.z, Finish.z, time) : Finish.z);
		}
		public Vector3TweenInstruction(Vector3 start, Vector3 finish, Func<float, float, float, float> easeFunction) :
			base(start, finish, easeFunction)
		{
		}
	}
}