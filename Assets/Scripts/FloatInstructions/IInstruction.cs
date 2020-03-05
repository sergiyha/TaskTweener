namespace FloatInstructions
{
	public interface IInstruction<T> where T : struct
	{
		T Calculate(float time);
		void SetEase(EasingFunction.Ease type);
		T GetStart();
		T GetFinish();
		void SwitchLastAndFirst();
	}
}