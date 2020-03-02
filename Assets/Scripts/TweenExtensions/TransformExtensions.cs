using Actions;
using FloatInstructions.ConcreteInstructions;
using Tweener;
using UnityEngine;

namespace TweenExtensions
{
	public static partial class TweenExtension 
	{
		public static ITaskTweener Scale(this Transform tr, Vector3 from, Vector3 to, float duration)
		{
			return new TaskTweener<Vector3>(
				duration,
				new Vector3TweenInstruction(from, to, EasingFunction.Linear),
				tr.ScaleAction());
		}

		public static ITaskTweener LocalPosition(this Transform tr, Vector3 from, Vector3 to, float duration)
		{
			return new TaskTweener<Vector3>(
				duration,
				new Vector3TweenInstruction(from, to, EasingFunction.Linear),
				tr.LocalPositionAction());
		}

		public static ITaskTweener Position(this Transform tr, Vector3 from, Vector3 to, float duration)
		{
			return new TaskTweener<Vector3>(
				duration,
				new Vector3TweenInstruction(from, to, EasingFunction.Linear),
				tr.PositionAction());
		}
	}
}
