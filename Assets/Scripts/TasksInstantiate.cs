﻿using System.Threading.Tasks;
using Tweener;
using TweenExtensions;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TasksInstantiate : MonoBehaviour
{
	public GameObject Object;
	public GameObject RefObject;
	
	private Task<string> _task;
	public float Duration;
	public float ScaleFactor;
	private ITaskTweener _tweener;
	private ITaskTweener _tweener_2;
	

	public Vector3 speed;

	private void Start()
	{
		//TestRotationEulers();
		TestRotation();

		
		// _tweener_2 = Object.transform.TweenScale(
		// 	new Vector3(localScale.x * ScaleFactor, localScale.y * ScaleFactor, localScale.z * ScaleFactor),
		// 	localScale,
		// 	Duration);
		
		Flow();
	}

	private void TestRotation()
	{
		var rotation = Object.transform.rotation;
		_tweener = Object.transform
			.TweenRotation(
				rotation, 
				RefObject.transform.rotation,
				Duration).
			SetOnCancel(() => { Debug.LogError("CANCEL");}).
			SetOnComplete(() => { Debug.LogError("COMPLETE");}).
			SetOnStart(() => { Debug.LogError("START"); }).
			SetOnUpdate(() => { Debug.LogError("UPDATE"); }).
			SetEaseType(EasingFunction.Ease.EaseInExpo);
	}

	private void TestRotationEulers()
	{
		var rotation = Object.transform.rotation.eulerAngles;
		_tweener = Object.transform
			.TweenRotationEuler(
				rotation, 
				new Vector3(720f, 0f, 0f),
				Duration).
			SetOnCancel(() => { Debug.LogError("CANCEL");}).
			SetOnComplete(() => { Debug.LogError("COMPLETE");}).
			SetOnStart(() => { Debug.LogError("START"); }).
			SetOnUpdate(() => { Debug.LogError("UPDATE"); }).
			SetEaseType(EasingFunction.Ease.Linear);
	}

	private void TestScaleTween()
	{
		var localScale = Object.transform.localScale;
		_tweener = Object.transform
			.TweenScale(
				localScale, 
				new Vector3(localScale.x * ScaleFactor, localScale.y * ScaleFactor, localScale.z * ScaleFactor),
				Duration).
			SetOnCancel(() => { Debug.LogError("CANCEL");}).
			SetOnComplete(() => { Debug.LogError("COMPLETE");}).
			SetOnStart(() => { Debug.LogError("START"); }).
			SetOnUpdate(() => { Debug.LogError("UPDATE"); }).
			SetEaseType(EasingFunction.Ease.EaseInExpo);
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