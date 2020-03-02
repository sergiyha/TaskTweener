using System;

namespace FloatInstructions
{
	public abstract class Instruction<T> : IInstruction<T> where T : struct
	{
		protected const float Precision = 0.00001f;
		protected Func<float, float, float, float> EaseFunction;
		protected T Start;
		protected T Finish;

		protected Instruction(T start, T finish, Func<float, float, float, float> easeFunction)
		{
			EaseFunction = easeFunction;
			Start = start;
			Finish = finish;
		}

		//from 0 to 1
		protected abstract T _calculate(float time);

		public T Calculate(float time)
		{
			return _calculate(time);
		}

		void IInstruction<T>.SetEase(EasingFunction.Ease type)
		{
			EaseFunction = EasingFunction.GetEasingFunction(type);
		}

		public T GetStart() => Start;
		public T GetFinish() => Finish;
	}
}