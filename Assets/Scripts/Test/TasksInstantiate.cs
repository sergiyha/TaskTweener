using System.Collections.Generic;
using System.Threading.Tasks;
using Tweener;
using TweenExtensions;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class TasksInstantiate : MonoBehaviour
{
	public Text ValueChangedTxt;
	public Image ColorImage;
	public GameObject[] Test_Objects;


	public float Duration;

	private List<ITaskTweener> Tweens;
	private ITaskTweener _taskTweener;

	public bool Example1;
	public bool Example2;

	private void Start()
	{
		if (Example1)
			Example_1();
		else if (Example2)
			Example_2();
	}

	private void Example_1()
	{
		Test_0_TransformRotation(-1).Start();
		Test_1_TransformPosition(-1).Start();
		Test_2_TransformScale(-1).Start();
		Test_4_ValueTween(-1).Start();
		Test_5_ValueTweenColor(-1).Start();
	}

	private async void Example_2()
	{
		await Test_0_TransformRotation(2).Start();
		await Test_1_TransformPosition(2).Start();
		await Test_2_TransformScale(2).Start();
		await Test_4_ValueTween(2).Start();
		await Test_5_ValueTweenColor(2).Start();
		Debug.LogError("All Tweens Done");
	}
	
	private ITaskTweener Test_0_TransformRotation(int loopsCount)
	{
		var _object = Test_Objects[0].transform;
		var objectRotation = _object.transform.rotation;
		return _object.transform.TweenRotation(objectRotation, new Quaternion(0, 0.7f, 0.5f, 0.5f), Duration)
			.SetLoop(loopsCount, LoopType.YoYo);
	}

	private ITaskTweener Test_1_TransformPosition(int loopsCount)
	{
		var _object = Test_Objects[1].transform;
		Transform transform1;
		return _object.transform
			.TweenPosition((transform1 = _object.transform).position, transform1.position + Vector3.down, Duration)
			.SetLoop(loopsCount, LoopType.YoYo);
	}

	private ITaskTweener Test_2_TransformScale(int loopsCount)
	{
		var _object = Test_Objects[2].transform;
		var localScale = _object.transform.localScale;
		return _object.transform.TweenScale(localScale, localScale + Vector3.one, Duration)
			.SetLoop(loopsCount, LoopType.YoYo);
	}

	private ITaskTweener Test_4_ValueTween(int loopsCount)
	{
		return TweenExtension.TweenValue(0, 10, Duration, (val) => ValueChangedTxt.text = val.ToString())
			.SetLoop(loopsCount, LoopType.YoYo);
	}

	private ITaskTweener Test_5_ValueTweenColor(int loopsCount)
	{
		return ColorImage.TweenColor(ColorImage.color, Color.magenta, Duration).SetLoop(loopsCount, LoopType.YoYo);
	}
}