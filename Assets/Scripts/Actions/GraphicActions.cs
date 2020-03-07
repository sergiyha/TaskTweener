using System;
using UnityEngine;
using UnityEngine.UI;

namespace Actions
{
	public static partial class ActionsLibrary
	{
		public static Action<Color> ColorAction(this Graphic gr)
		{
			return updatedScale => { gr.color = updatedScale; };
		}

	}
}
