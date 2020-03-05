using System;
using System.Linq;
using System.Threading.Tasks;
using FloatInstructions;
using UnityEngine;

namespace Tweener
{
	public class TaskCurveTween<T> : ATaskTween<T> where T : struct
	{
		private readonly AnimationCurve _curve;
		private float _startCurveTime;
		private float _finalCurveTime;
		private float _curveTimeDelta;

		private float _maxCurvValue;
		private float _minCurvValue;
		private float _curveValueDelta;
		private float _inverseCurveValueDelta;

		private float _inverseDuration;


		public TaskCurveTween(
			IInstruction<T> instruction,
			Action<T> applyAction,
			AnimationCurve curve,
			float duration) :
			base(
				instruction,
				applyAction)
		{
			_curve = curve;
			Duration = duration;
			_inverseDuration = 1 / Duration;
			CalculateCurveData();
		}

		private void CalculateCurveData()
		{
			_startCurveTime = _curve.keys.First().time;
			_finalCurveTime = _curve.keys.Last().time;
			_curveTimeDelta = _finalCurveTime - _startCurveTime;

			_maxCurvValue = _curve.keys.Max(k => k.value);
			_minCurvValue = _curve.keys.Min(k => k.value);
			_curveValueDelta = _maxCurvValue - _minCurvValue;
			_inverseCurveValueDelta = 1 / _curveValueDelta;
		}

		protected override async Task Loop()
		{
			do
			{
				for (float i = 0; i <= Duration; i += Time.deltaTime)
				{
					if (ShouldBeCanceled)
					{
						CancelEvt?.Invoke();
						break;
					}

					var time = Mathf.Lerp(_startCurveTime, _finalCurveTime, i * _inverseDuration);
					var lerpValue = (_curve.Evaluate(time) - _minCurvValue) * _inverseCurveValueDelta;
					var inbetweening = Instruction.Calculate(lerpValue);
					Apply(inbetweening);
					await Task.Yield();
					if (!Application.isPlaying) return;
				}

				if (LoopCount > 0)
				{
					LoopCount--;
				}

				CheckLoopEnd();
			} while (LoopCount != 0);

			if (!ShouldBeCanceled)
			{
				Apply?.Invoke(Instruction.Calculate(_curve.Evaluate(_finalCurveTime)));
				CompleteEvt?.Invoke();
			}
		}
	}
}