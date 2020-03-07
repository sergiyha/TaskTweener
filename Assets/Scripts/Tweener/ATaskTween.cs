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
		protected int InitialLoopCount = 1;
		protected LoopType LoopType = LoopType.Restart;
		protected float Duration;
		protected float InverseDuration;


		private Task _tweener;
		private float _currentTweenTime;
		protected readonly IInstruction<T> Instruction;
		protected Action<T> Apply;
		protected Action StartEvt;
		protected Action StopEvt;
		protected Action CompleteEvt;
		protected Action UpdateEvt;
		protected bool ShouldBeStopped = false;
		protected bool IsPaused = false;

		protected abstract void TweenStrategy(float tweenTime);

		private async Task Loop()
		{
			StartEvt?.Invoke();
			do
			{
				for (_currentTweenTime = 0; _currentTweenTime < Duration; _currentTweenTime += Time.deltaTime)
				{
					if (CheckShouldBeStopped())
					{
						return;
					}

					TweenStrategy(_currentTweenTime);
					UpdateEvt?.Invoke();
					await Task.Yield();
					if (!Application.isPlaying) return;

					if (await WaitPaused())
					{
						return;
					}
				}

				if (LoopCount > 0)
				{
					LoopCount--;
				}

				CheckLoopEnd();
			} while (LoopCount != 0);


			if (!ShouldBeStopped)
			{
				Apply?.Invoke(Instruction.GetFinish());
				CompleteEvt?.Invoke();
				Reset();
			}
		}

		protected void CheckLoopEnd()
		{
			if (LoopCount == 0) return;

			if (LoopType == LoopType.YoYo)
			{
				Instruction.SwitchLastAndFirst();
			}
		}

		private bool CheckShouldBeStopped()
		{
			if (ShouldBeStopped)
			{
				StopEvt?.Invoke();
				Reset();
				return true;
			}

			return false;
		}

		private async Task<bool> WaitPaused()
		{
			while (IsPaused)
			{
				await Task.Yield();
				var shouldStop = CheckShouldBeStopped();
				if (shouldStop)
					return true;
			}

			return false;
		}

		ITaskTweener ITaskTweener.SetOnStart(Action onStart)
		{
			StartEvt = onStart;
			return this;
		}

		ITaskTweener ITaskTweener.SetOnComplete(Action onComplete)
		{
			CompleteEvt += onComplete;
			return this;
		}

		public ITaskTweener SetOnStopped(Action onStop)
		{
			StopEvt += onStop;
			return this;
		}

		ITaskTweener ITaskTweener.SetOnUpdate(Action onUpdate)
		{
			UpdateEvt += onUpdate;
			return this;
		}

		ITaskTweener ITaskTweener.SetEaseType(EasingFunction.Ease type)
		{
			Instruction.SetEase(type);
			return this;
		}

		ITaskTweener ITaskTweener.SetLoop(int count, LoopType type)
		{
			InitialLoopCount = count;
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

		void ITaskTweener.Stop()
		{
			ShouldBeStopped = true;
		}

		public void Reset()
		{
			Instruction.ResetInstruction();
			Apply(Instruction.GetStart());
			LoopCount = InitialLoopCount;
			_currentTweenTime = 0;
		}

		public void Pause()
		{
			IsPaused = true;
		}

		public void Resume()
		{
			IsPaused = false;
		}
	}
}