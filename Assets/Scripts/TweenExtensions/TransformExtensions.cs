using Actions;
using FloatInstructions.ConcreteInstructions;
using Tweener;
using UnityEngine;

namespace TweenExtensions
{
	public static partial class TweenExtension
	{
		public static ITaskTweener TweenScale(this Transform tr, Vector3 from, Vector3 to, float duration)
		{
			return new TaskTweener<Vector3>(
				duration,
				new Vector3TweenInstruction(from, to, EasingFunction.Linear),
				tr.ScaleAction());
		}

		public static ITaskTweener TweenLocalPosition(this Transform tr, Vector3 from, Vector3 to, float duration)
		{
			return new TaskTweener<Vector3>(
				duration,
				new Vector3TweenInstruction(from, to, EasingFunction.Linear),
				tr.LocalPositionAction());
		}

		public static ITaskTweener TweenPosition(this Transform tr, Vector3 from, Vector3 to, float duration)
		{
			return new TaskTweener<Vector3>(
				duration,
				new Vector3TweenInstruction(from, to, EasingFunction.Linear),
				tr.PositionAction());
		}

		public static ITaskTweener TweenRotationEuler(this Transform tr, Vector3 from, Vector3 to, float duration)
		{
			return new TaskTweener<Vector3>(
				duration,
				new Vector3TweenInstruction(from, to, EasingFunction.Linear),
				tr.RotationEulerAction());
		}

		public static ITaskTweener TweenRotation(this Transform tr, Quaternion from, Quaternion to, float duration)
		{
			return new TaskTweener<Quaternion>(
				duration,
				new QuaternionInstruction(from, to, EasingFunction.Linear),
				tr.RotationAction());
		}
	}
}