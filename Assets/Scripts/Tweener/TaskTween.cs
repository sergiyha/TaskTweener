using System;
using System.Threading.Tasks;
using FloatInstructions;
using UnityEngine;

namespace Tweener
{
	public class TaskTween<T> : ATaskTween<T> where T : struct
	{
		public TaskTween(
			float duration,
			IInstruction<T> instruction,
			Action<T> applyAction) : base(instruction, applyAction, duration)
		{
		}

		protected override void TweenStrategy(float tweenTime)
		{
			var inbetweening = Instruction.Calculate(tweenTime * InverseDuration);
			Apply?.Invoke(inbetweening);
		}
	}
}