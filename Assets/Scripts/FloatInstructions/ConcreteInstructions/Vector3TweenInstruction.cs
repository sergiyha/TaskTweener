using System;
using UnityEngine;

namespace FloatInstructions.ConcreteInstructions
{
    public class Vector3TweenInstruction : Instruction<Vector3>
    {
        protected override Vector3 _calculate(float time)
        {
            return new Vector3(
                EaseFunction(Start.x, Finish.x, time),
                EaseFunction(Start.y, Finish.y, time),
                EaseFunction(Start.z, Finish.z, time));
        }

        public Vector3TweenInstruction(Vector3 start, Vector3 finish, Func<float, float, float, float> easeFunction) :
            base(start, finish, easeFunction)
        {
        }
    }
}
