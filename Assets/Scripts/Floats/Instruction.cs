using System;

namespace Floats
{
	public abstract class Instruction<T> : IInstruction where T : struct
	{
		public Func<float, float, int, float> EaseFunction;
		public T Start;
		public T Finish;
		//from 0 to 1
		protected abstract T Calculate(float time);
		
		public object Calcucate(float time)
		{
			return Calculate(time);
		}
	}
	
	public interface IInstruction
	{
		object Calcucate(float time);
	}
}
