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


		private float _maxCurvValue;
		private float _minCurvValue;
		private float _curveValueDelta;
		private float _inverseCurveValueDelta;


		public TaskCurveTween(
			IInstruction<T> instruction,
			Action<T> applyAction,
			AnimationCurve curve,
			float duration) :
			base(
				instruction,
				applyAction, 
				duration)
		{
			_curve = curve;

			CalculateCurveData();
		}

		private void CalculateCurveData()
		{
			_startCurveTime = _curve.keys.First().time;
			_finalCurveTime = _curve.keys.Last().time;

			_maxCurvValue = _curve.keys.Max(k => k.value);
			_minCurvValue = _curve.keys.Min(k => k.value);
			_curveValueDelta = _maxCurvValue - _minCurvValue;
			_inverseCurveValueDelta = 1 / _curveValueDelta;
		}

		protected override void TweenStrategy(float tweenTime)
		{
			var time = Mathf.Lerp(_startCurveTime, _finalCurveTime, tweenTime * InverseDuration);
			var lerpValue = (_curve.Evaluate(time) - _minCurvValue) * _inverseCurveValueDelta;
			var inbetweening = Instruction.Calculate(lerpValue);
			Apply(inbetweening);
		}
	}
}