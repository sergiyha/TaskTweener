using System;
using FloatInstructions.ConcreteInstructions;
using Tweener;

namespace TweenExtensions
{
	public static partial class TweenExtension
	{
		public static ITaskTweener TweenValue(float start, float end, float duration, Action<float> onValueUpdate)
		{
			return new TaskTweener<float>(duration, new FloatTweenInstruction(start, end, EasingFunction.Linear),
				onValueUpdate);
		}
	}
}