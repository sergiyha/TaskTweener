using System.Threading.Tasks;
using Tweener;
using TweenExtensions;
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
		var localScale = Object.transform.localScale;
		_tweener = Object.transform
			.Scale(
				localScale, 
				new Vector3(localScale.x * ScaleFactor, localScale.y * ScaleFactor, localScale.z * ScaleFactor),
				Duration).
			SetOnCancel(() => { Debug.LogError("CANCEL");}).
			SetOnComplete(() => { Debug.LogError("COMPLETE");}).
			SetOnStart(() => { Debug.LogError("START"); }).
			SetOnUpdate(() => { Debug.LogError("UPDATE"); }).
			SetEaseType(EasingFunction.Ease.EaseInExpo);

		_tweener_2 = Object.transform.Scale(
			new Vector3(localScale.x * ScaleFactor, localScale.y * ScaleFactor, localScale.z * ScaleFactor),
			localScale,
			Duration);
		
		Flow();
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