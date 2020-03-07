using System;
using System.Threading.Tasks;
using FloatInstructions;
using UnityEngine;

namespace Tweener
{
	public abstract class ATaskTween<T> : ITaskTweener where T : struct
	{
		protected ATaskTween(
			IInstruction<T> instruction,
			Action<T> applyAction,
			float duration)
		{
			Instruction = instruction;
			Apply = applyAction;
			Duration = duration;
			InverseDuration = 1 / Duration;
		}

		protected int LoopCount = 1;
		protected LoopType LoopType = LoopType.Restart;
		protected float Duration;
		protected float InverseDuration;

		private Task _tweener;
		protected readonly IInstruction<T> Instruction;
		protected Action<T> Apply;
		protected Action StartEvt;
		protected Action CancelEvt;
		protected Action CompleteEvt;
		protected Action UpdateEvt;
		protected bool ShouldBeCanceled = false;

		protected abstract void TweenStrategy(float tweenTime);

		private async Task Loop()
		{
			StartEvt?.Invoke();
			do
			{
				for (float i = 0; i < Duration; i += Time.deltaTime)
				{
					if (ShouldBeCanceled)
					{
						CancelEvt?.Invoke();
						ResetTween();
						break;
					}

					TweenStrategy(i);

					UpdateEvt?.Invoke();
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
				Apply?.Invoke(Instruction.GetFinish());
				CompleteEvt?.Invoke();
				ResetTween();
			}
		}

		private void ResetTween()
		{
			StartEvt = null;
			CancelEvt = null;
			CompleteEvt = null;
			UpdateEvt = null;
		}

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