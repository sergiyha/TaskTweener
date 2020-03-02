using System;
using UnityEngine;

namespace Actions
{
    public  static partial class ActionsLibrary
    {
        public static Action<Vector3> ScaleAction(this Transform transform)
        {
            return (Vector3 updatedScale) => { transform.localScale = updatedScale; };
        }

        public static Action<Vector3> LocalPositionAction(this Transform transform)
        {
            return (Vector3 updatedLocalPosition) => { transform.localPosition = updatedLocalPosition; };
        }

        public static Action<Vector3> PositionAction(this Transform transform)
        {
            return (Vector3 updatedPosition) => { transform.position = updatedPosition; };
        }
    }
}
