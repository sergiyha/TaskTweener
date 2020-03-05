using System;
using System.Threading.Tasks;
using FloatInstructions;

namespace Tweener
{
	public abstract class ATaskTween<T> : ITaskTweener where T : struct
	{
		protected ATaskTween(
			IInstruction<T> instruction,
			Action<T> applyAction)
		{
			Instruction = instruction;
			Apply = applyAction;
		}

		protected int LoopCount = 1;
		protected LoopType LoopType = LoopType.Restart;
		protected  float Duration;

		private Task _tweener;
		protected readonly IInstruction<T> Instruction;
		protected Action<T> Apply;
		protected Action StartEvt;
		protected Action CancelEvt;
		protected Action CompleteEvt;
		protected Action UpdateEvt;
		protected bool ShouldBeCanceled = false;

		protected abstract Task Loop();

		protected void CheckLoopEnd()
		{
			if (LoopCount == 0) return;

			if (LoopType == LoopType.YoYo)
			{
				Instruction.SwitchLastAndFirst();
			}
		}
		
		ITaskTweener ITaskTweener.SetOnStart(Action onStart)
		{
			StartEvt = onStart;
			return this;
		}

		ITaskTweener ITaskTweener.SetOnComplete(Action onComplete)
		{
			CompleteEvt = onComplete;
			return this;
		}

		ITaskTweener ITaskTweener.SetOnCancel(Action onCancel)
		{
			CancelEvt = onCancel;
			return this;
		}

		ITaskTweener ITaskTweener.SetOnUpdate(Action onUpdate)
		{
			UpdateEvt = onUpdate;
			return this;
		}

		ITaskTweener ITaskTweener.SetEaseType(EasingFunction.Ease type)
		{
			Instruction.SetEase(type);
			return this;
		}

		ITaskTweener ITaskTweener.SetLoop(int count, LoopType type)
		{
			LoopCount = count;
			LoopType = type;
			return this;
		}

		public Task Start()
		{
			_tweener = Loop();
			return _tweener;
		}

		public async Task Start(int delay)
		{
			await Task.Delay(delay);
			await Start();
		}

		void ITaskTweener.Cancel()
		{
			ShouldBeCanceled = true;
		}
	}
}