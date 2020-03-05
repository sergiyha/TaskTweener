using System;
using FloatInstructions.ConcreteInstructions;
using Tweener;
using UnityEngine;

namespace TweenExtensions
{
	public static partial class TweenExtension
	{
		public static ITaskTweener TweenValue(float start, float end, float duration, Action<float> onValueUpdate)
		{
			return new TaskTween<float>(
				duration, 
				new FloatTweenInstruction(start, end, EasingFunction.Linear),
				onValueUpdate);
		}

		public static ITaskTweener TweenCurveValue(float start, float end, AnimationCurve curve,float duration,
			Action<float> onValueUpdate)
		{
			return new TaskCurveTween<float>(
				new FloatTweenInstruction(start, end, EasingFunction.Linear),
				onValueUpdate,
				curve,
				duration);
		}
	}
}