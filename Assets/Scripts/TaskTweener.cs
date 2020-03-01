using System;
using System.Threading.Tasks;
using UnityEngine;

public class TaskTweener<T> : ITaskTweener where T : struct
{
	public TaskTweener(
		T start,
		T finish,
		float duration,
		Func<T, T, float, T> instruction,
		Action<T> ApplyAction,
		Action onComplete = null,
		Action onCanceled = null)
	{
		_start = start;
		_finish = finish;
		_duration = duration;
		_instruction = instruction;
		Apply = ApplyAction;
		_onComplete = onComplete;
		_onCanceled = onCanceled;
	}


	private Task Tweener;
	private bool _shouldBeCanceled;
	private Action _onComplete;
	private Action _onCanceled;

	private Action<T> Apply;

	private float _duration;
	private T _start;
	private T _finish;
	private Func<T, T, float, T> _instruction;

	public event Action StartEvt;
	public event Action CancelEvt;
	public event Action FinishEvt;
	public event Action UpdateEvt;
	public event Action OnUpdate;


	async Task Tween()
	{
		for (float i = 0; i < _duration; i += Time.deltaTime)
		{
			if (_shouldBeCanceled)
			{
				_onCanceled?.Invoke();
				break;
			}

			var scale = _instruction(_start, _finish, i / _duration);
			Apply?.Invoke(scale);
			await Task.Yield();
		}

		if (!_shouldBeCanceled)
		{
			_onComplete?.Invoke();
		}
	}

	public void Cancel()
	{
		_shouldBeCanceled = true;
	}


	public Task Start()
	{
		Tweener = Tween();
		return Tweener;
	}

	public async Task Start(int delay)
	{
		await Task.Delay(delay);
		await Start();
	}
}

public interface ITaskTweener
{
	event Action StartEvt;
	event Action CancelEvt;
	event Action FinishEvt;

	event Action UpdateEvt;

	event Action OnUpdate;
	Task Start();
	Task Start(int delay);
	void Cancel();
}