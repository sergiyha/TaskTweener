using System;
using System.Threading.Tasks;
using FloatInstructions;
using UnityEngine;

namespace Tweener
{
	public class TaskTween<T> : ATaskTween<T> where T : struct
	{
		public TaskTween(
			float duration,
			IInstruction<T> instruction,
			Action<T> applyAction) : base(instruction,applyAction)
		{
			Duration = duration;
		}
		
		protected override async Task Loop()
		{
			StartEvt?.Invoke();
			do
			{
				for (float i = 0; i < Duration; i += Time.deltaTime)
				{
					if (ShouldBeCanceled)
					{
						CancelEvt?.Invoke();
						break;
					}

					
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
			}
		}
	}
}