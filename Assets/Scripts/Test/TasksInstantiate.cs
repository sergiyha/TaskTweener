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


	public float Duration;

	public GameObject[] Test_Objects;
	private List<ITaskTweener> Tweens;
	
	private void Start()
	{
		//ResetTweens(0);
		Test_0_TransformRotation().Start();
		Test_1_TransformPosition().Start();
		Test_2_TransformScale().Start();
		Test_4_ValueTween().Start();
		Test_5_ValueTweenColor().Start();
	}

	private async void ResetTweens(int fromIndex)
	{
		foreach (var tween in Tweens)
		{
			tween.Cancel();
		}

		for (var i = fromIndex; i < Tweens.Count; i++)
		{
			await Tweens[i].Start();
		}

		Debug.LogError("Tweening Finished");
	}

	private ITaskTweener Test_0_TransformRotation()
	{
 		var _object = Test_Objects[0].transform;
		var objectRotation = _object.transform.rotation;
		return _object.transform.TweenRotation(objectRotation, new Quaternion(0, 0.7f, 0.5f, 0.5f), Duration)
			.SetLoop(-1, LoopType.YoYo);
	}

	private ITaskTweener Test_1_TransformPosition()
	{
		var _object = Test_Objects[1].transform;
		Transform transform1;
		return _object.transform
			.TweenPosition((transform1 = _object.transform).position, transform1.position + Vector3.down, Duration)
			.SetLoop(-1, LoopType.YoYo);
	}

	private ITaskTweener Test_2_TransformScale()
	{
		var _object = Test_Objects[2].transform;
		var localScale = _object.transform.localScale;
		return _object.transform.TweenScale(localScale, localScale + Vector3.one, Duration).SetLoop(2, LoopType.YoYo);
	}

	private ITaskTweener Test_4_ValueTween()
	{
		return TweenExtension.TweenValue(0, 10, Duration, (val) => ValueChangedTxt.text = val.ToString())
			.SetLoop(-1, LoopType.YoYo);
	}

	private ITaskTweener Test_5_ValueTweenColor()
	{
		return ColorImage.TweenColor(ColorImage.color, Color.magenta, Duration).SetLoop(-1, LoopType.YoYo);
	}
}