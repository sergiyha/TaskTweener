using System;
using System.Threading.Tasks;

namespace Tweener
{
	public interface ITaskTweener
	{
		ITaskTweener SetOnStart(Action onStart);
		ITaskTweener SetOnComplete(Action onComplete);
		ITaskTweener SetOnStopped(Action onStop);
		ITaskTweener SetOnUpdate(Action onUpdate);
		ITaskTweener SetEaseType(EasingFunction.Ease type);

		/// <summary>
		/// Set the appropriate type and count of loops
		/// if YoYo type is selected the and loop count is not divided by 2 there will be half of yoyo animation
		/// Ex: if c = 1 and type = YoYo
		/// Result: forward animation without same animation in opposite direction 
		/// </summary>
		/// <param name="count"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		ITaskTweener SetLoop(int count, LoopType type);

		Task Start();
		void Reset();
		Task Start(int delay);
		void Stop();

		void Pause();
		void Resume();
	}
}