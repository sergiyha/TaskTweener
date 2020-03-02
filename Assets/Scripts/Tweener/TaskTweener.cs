using System;
using System.Threading.Tasks;
using FloatInstructions;
using UnityEngine;

namespace Tweener
{
	public class TaskTweener<T> : ITaskTweener where T : struct
	{
		public TaskTweener(
			float duration,
			IInstruction<T> instruction,
			Action<T> applyAction)
		{
			_duration = duration;
			_instruction = instruction;
			Apply = applyAction;
		}

		private Task _tweener;
		private bool _shouldBeCanceled;

		private Action<T> Apply;
		private IInstruction<T> _instruction;
		private float _duration;

		private Action _startEvt;
		private Action _cancelEvt;
		private Action _completeEvt;
		private Action _updateEvt;

		async Task Tween()
		{
			_startEvt?.Invoke();

			for (float i = 0; i < _duration; i += Time.deltaTime)
			{
				if (_shouldBeCanceled)
				{
					_cancelEvt?.Invoke();
					break;
				}

				var inbetweening = _instruction.Calculate(i / _duration);
				Apply?.Invoke(inbetweening);
				_updateEvt?.Invoke();

				await Task.Yield();
				if (!Application.isPlaying) return;
			}

			if (!_shouldBeCanceled)
			{
				_completeEvt?.Invoke();
			}
		}

		public void Cancel()
		{
			_shouldBeCanceled = true;
		}

		public ITaskTweener SetOnStart(Action onStart)
		{
			_startEvt = onStart;
			return this;
		}

		public ITaskTweener SetOnComplete(Action onComplete)
		{
			_completeEvt = onComplete;
			return this;
		}

		public ITaskTweener SetOnCancel(Action onCancel)
		{
			_cancelEvt = onCancel;
			return this;
		}

		public ITaskTweener SetOnUpdate(Action onUpdate)
		{
			_updateEvt = onUpdate;
			return this;
		}

		public ITaskTweener SetEaseType(EasingFunction.Ease type)
		{
			_instruction.SetEase(type);
			return this;
		}

		public Task Start()
		{
			_tweener = Tween();
			return _tweener;
		}

		public async Task Start(int delay)
		{
			await Task.Delay(delay);
			await Start();
		}
	}
}