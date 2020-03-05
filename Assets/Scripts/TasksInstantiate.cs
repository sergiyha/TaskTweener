using System.Threading.Tasks;
using Actions;
using Tweener;
using TweenExtensions;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TasksInstantiate : MonoBehaviour
{
	public GameObject Object;
	public GameObject RefObject;
	public AnimationCurve Curve;

	private Task<string> _task;
	public float Duration;
	public float ScaleFactor;
	private ITaskTweener _tweener;
	private ITaskTweener _tweener_2;

	public GameObject[] gos;


	public Vector3 speed;

	private void Start()
	{
		for (int i = 0; i < gos.Length; i++)
		{
			TestRotation(gos[i]);
			//TestScaleTween(gos[i]);
		}
		//TestValueCurve();
		//TestRotationEulers();
		//TestRotation();
		//TestValue();


		// _tweener_2 = Object.transform.TweenScale(
		// 	new Vector3(localScale.x * ScaleFactor, localScale.y * ScaleFactor, localScale.z * ScaleFactor),
		// 	localScale,
		// 	Duration);

		//Flow();
	}

	private void TestValue()
	{
		_tweener = TweenExtension.TweenValue(0, 10, 10, (v) => { Debug.LogError(v); });
	}

	private void TestValueCurve()
	{
		_tweener = TweenExtension.TweenCurveValue(
			0f,
			20f,
			Curve, 3f,
			(a) =>
			{
				Vector3 localScale = Object.transform.localScale;
				localScale = new Vector3(a, localScale.y, localScale.z);
				Object.transform.localScale = localScale;
			}).SetLoop(-1, LoopType.YoYo);
	}

	[ContextMenu("Test rotation")]
	private void TestRotation(GameObject go)
	{
		var rotation = Object.transform.rotation;
		go.transform
			.TweenRotation(
				rotation,
				RefObject.transform.rotation,
				Duration)//.SetOnCancel(() => { Debug.LogError("CANCEL"); })
		//	.SetOnComplete(() => { Debug.LogError("COMPLETE"); }).SetOnStart(() => { Debug.LogError("START"); })
//			.SetOnUpdate(() => { Debug.LogError("UPDATE"); }).SetEaseType(EasingFunction.Ease.EaseInExpo)
			.SetLoop(-1, LoopType.YoYo).Start();
		//Flow();
	}

	private void TestRotationEulers()
	{
		var rotation = Object.transform.rotation.eulerAngles;
		_tweener = Object.transform
			.TweenRotationEuler(
				rotation,
				new Vector3(720f, 0f, 0f),
				Duration).SetOnCancel(() => { Debug.LogError("CANCEL"); })
			.SetOnComplete(() => { Debug.LogError("COMPLETE"); }).SetOnStart(() => { Debug.LogError("START"); })
			.SetOnUpdate(() => { Debug.LogError("UPDATE"); }).SetEaseType(EasingFunction.Ease.Linear);
	}

	private void TestScaleTween(GameObject go)
	{
		var localScale = go.transform.localScale;
		go.transform
			.TweenScale(
				localScale,
				new Vector3(localScale.x * ScaleFactor, localScale.y * ScaleFactor, localScale.z * ScaleFactor),
				Duration).SetLoop(-1, LoopType.YoYo).Start(); //SetOnCancel(() => { Debug.LogError("CANCEL"); })
		//.SetOnComplete(() => { Debug.LogError("COMPLETE"); }).SetOnStart(() => { Debug.LogError("START"); })
		//.SetOnUpdate(() => { Debug.LogError("UPDATE"); }).SetEaseType(EasingFunction.Ease.EaseInExpo);
	}

	async Task Flow()
	{
		await Task.Delay(2000);
		await _tweener.Start();
		//await _tweener_2.Start();
	}

	async Task Stop()
	{
		await Task.Delay(2000);
		_tweener.Cancel();
	}

	private void Update()
	{
		// var rotation = Object.transform.rotation;
		// Object.transform.Rotate(new Vector3(speed.x, speed.y, speed.z));
	}
}