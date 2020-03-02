using System;
using System.Threading.Tasks;

namespace Tweener
{
	public interface ITaskTweener
	{
		ITaskTweener SetOnStart(Action onStart);
		ITaskTweener SetOnComplete(Action onComplete);
		ITaskTweener SetOnCancel(Action onCancel);
		ITaskTweener SetOnUpdate(Action onUpdate);
		ITaskTweener SetEaseType(EasingFunction.Ease type);
		Task Start();
		Task Start(int delay);
		void Cancel();
	}
}