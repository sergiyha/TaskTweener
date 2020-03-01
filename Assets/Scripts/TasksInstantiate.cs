using System;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TasksInstantiate : MonoBehaviour
{
	public GameObject Object;
	private Task<string> _task;
	public float Duration;
	public float ScaleFactor;
	private ITaskTweener _tweener;
	private ITaskTweener _tweener_2;

	public Vector3 speed;

	private void Start()
	{
		
		EasingFunction.Ease ease = EasingFunction.Ease.EaseInOutQuad;


		var localScale = Object.transform.localScale;
		_tweener = Object.transform.Scale(localScale,
			new Vector3(localScale.x * ScaleFactor, localScale.y * ScaleFactor, localScale.z * ScaleFactor), Duration);
		_tweener_2 = Object.transform.Scale(
			new Vector3(localScale.x * ScaleFactor, localScale.y * ScaleFactor, localScale.z * ScaleFactor),
			localScale,
			Duration);


		// _tweener_2 = new TaskTweener(
		// 	Object.GetComponent<Transform>(),
		// 	new Vector3(localScale.x * ScaleFactor, localScale.y * ScaleFactor, localScale.z * ScaleFactor),
		// 	localScale,
		// 	Duration,
		// 	(v1, v2, l) => Vector3.Lerp(v1, v2, l),
		// 	Object.GetComponent<Transform>().ScaleAction(),
		// 	() => Debug.LogError("Completed 1"),
		// 	() => Debug.LogError("Canceled 1"));

		Flow();
		//Stop();
	}

	async Task Flow()
	{
		await Task.Delay(2000);
		await _tweener.Start();
		await _tweener_2.Start();
	}

	async Task Stop()
	{
		await Task.Delay(2000);
		_tweener.Cancel();
	}

	private void Update()
	{
		var rotation = Object.transform.rotation;
		Object.transform.Rotate(new Vector3(speed.x, speed.y, speed.z));
	}
}

