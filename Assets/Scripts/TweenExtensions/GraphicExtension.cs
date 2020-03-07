using Actions;
using FloatInstructions.ConcreteInstructions;
using Tweener;
using UnityEngine;
using UnityEngine.UI;

namespace TweenExtensions
{
	public static partial class TweenExtension
	{
		public static ITaskTweener TweenColor(this Graphic gr, Color from, Color to, float duration)
		{
			return new TaskTween<Color>(
				duration,
				new ColorTweenInstruction(from, to, EasingFunction.Linear), gr.ColorAction());
		}
	}
}