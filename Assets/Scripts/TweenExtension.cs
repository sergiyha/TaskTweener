using System;
using UnityEngine;

public static class TweenExtension
{
	#region Transform

	public static ITaskTweener Scale(this Transform tr, Vector3 from, Vector3 to, float duration)
	{
		return new TaskTweener<Vector3>(from, to, duration, FunctionsLibrary.Linear(), tr.ScaleAction());
	}

	public static ITaskTweener LocalPosition(this Transform tr, Vector3 from, Vector3 to, float duration)
	{
		return new TaskTweener<Vector3>(from, to, duration, FunctionsLibrary.Linear(), tr.LocalPositionAction());
	}

	public static ITaskTweener Position(this Transform tr, Vector3 from, Vector3 to, float duration)
	{
		return new TaskTweener<Vector3>(from, to, duration, FunctionsLibrary.Linear(), tr.PositionAction());
	}

	#endregion
}

public static class ActionsLibrary
{
	public static Action<Vector3> ScaleAction(this Transform transform)
	{
		return (Vector3 updatedScale) => { transform.localScale = updatedScale; };
	}

	public static Action<Vector3> LocalPositionAction(this Transform transform)
	{
		return (Vector3 updatedLocalPosition) => { transform.localPosition = updatedLocalPosition; };
	}

	public static Action<Vector3> PositionAction(this Transform transform)
	{
		return (Vector3 updatedPosition) => { transform.position = updatedPosition; };
	}
}

public static class FunctionsLibrary
{
	public static Func<Vector3, Vector3, float, Vector3> Linear()
	{
		return (Vector3 s, Vector3 f, float value ) => new Vector3(
			EasingFunction.Linear(s.x,f.x, value),
			EasingFunction.Linear(s.y,f.y, value),
			EasingFunction.Linear(s.z,f.z, value));
	}
	
	public static Func<float, float, float, float> LinearFloat()
	{
		return EasingFunction.Linear;
	}
}