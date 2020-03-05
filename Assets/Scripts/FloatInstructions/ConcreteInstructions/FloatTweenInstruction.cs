using System;

namespace FloatInstructions.ConcreteInstructions
{
    public class FloatTweenInstruction : Instruction<float>
    {
        protected override float _calculate(float time)
        {
            return EaseFunction(Start, Finish, time);
        }

        public FloatTweenInstruction(float start, float finish, Func<float, float, float, float> easeFunction) : base(
            start, finish, easeFunction)
        {
        }
    }
    
    //Todo: think about generic realization of this class
    // public class GenericTweenInstruction<T> : Instruction<T> where T : struct
    // {
    //     protected override T _calculate(float time)
    //     {
    //         return EaseFunction(Start, Finish, time);
    //     }
    //
    //     public GenericTweenInstruction(T start, T finish, Func<float, float, float, float> easeFunction) : base(
    //         start, finish, easeFunction)
    //     {
    //     }
    // }
}
