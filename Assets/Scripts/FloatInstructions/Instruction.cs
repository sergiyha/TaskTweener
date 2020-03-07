using System;

namespace FloatInstructions
{
	public abstract class Instruction<T> : IInstruction<T> where T : struct
	{
		protected const float Precision = 0.00001f;
		protected Func<float, float, float, float> EaseFunction;
		protected T Start;
		protected T Finish;
		private InitialInstructionData<T> _instructionInitialData;

		protected Instruction(T start, T finish, Func<float, float, float, float> easeFunction)
		{
			EaseFunction = easeFunction;
			Start = start;
			Finish = finish;
			_instructionInitialData = new InitialInstructionData<T>(Start,Finish);
		}

		//from 0 to 1
		protected abstract T _calculate(float time);

		public void ResetInstruction()
		{
			Start = _instructionInitialData.Start;
			Finish = _instructionInitialData.End;
		}

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

		public void SwitchLastAndFirst()
		{
			var s = Start;
			Start = Finish;
			Finish = s;
		}
	}
	
	public class InitialInstructionData<T> where T : struct
	{
		public T Start;
		public T End;

		public InitialInstructionData(T getStart,T getFinish)
		{
			Start = getStart;
			End = getFinish;
		}
	}
}