using System;
using JetBrains.Annotations;

namespace Sucrose.Animation
{
    [PublicAPI]
    public static class AnimationExtensions
    {
        public static SucroseChildMotion WithMotion(this SucroseChildMotion motion, SucroseAnimation animation)
        {
            motion.WithMotion(animation.Animation);
            return motion;
        }
        
        public static SucroseChildMotion WithMotion(this SucroseChildMotion motion, SucroseAnimationBuilder builder)
        {
            SucroseAnimation local = new(motion.Sucrose);
            builder.Invoke(local);
            motion.WithMotion(local.Animation);
            return motion;
        }
        
        public static SucroseState WithMotion(this SucroseState state, SucroseAnimationBuilder builder)
        {
            SucroseAnimation local = new(state.Sucrose);
            builder.Invoke(local);
            state.WithMotion(local.Animation);
            return state;
        }

        public static SucroseState WithMotion(this SucroseState motion, SucroseAnimation animation)
        {
            motion.WithMotion(animation.Animation);
            return motion;
        }

        public static SucroseAnimation WithBinaryCurve(this SucroseAnimation animation, string path, Type type, string name, float zero, float one)
        {
            return animation.WithCurve(path, type, name, curve =>
            {
                curve.AddKeyframe(0f, zero);
                curve.AddKeyframe(1f / 60f, one);
            });
        }
        
        public static SucroseAnimation WithBinaryCurve(this SucroseAnimation animation, string path, Type type, string name, float zero)
        {
            return animation.WithBinaryCurve(path, type, name, zero, zero);
        }

        public static SucroseAnimation WithBinaryCurve<T>(this SucroseAnimation animation, string path, string name, float zero, float one)
        {
            return animation.WithBinaryCurve(path, typeof(T), name, zero, one);
        }
        
        public static SucroseAnimation WithBinaryCurve(this SucroseAnimation animation, Type type, string name, float zero, float one)
        {
            return animation.WithBinaryCurve(string.Empty, type, name, zero, one);
        }
        
        public static SucroseAnimation WithBinaryCurve<T>(this SucroseAnimation animation, string name, float zero, float one)
        {
            return animation.WithBinaryCurve(string.Empty, typeof(T), name, zero, one);
        }
        
        public static SucroseAnimation WithBinaryCurve<T>(this SucroseAnimation animation, string path, string name, float zero)
        {
            return animation.WithBinaryCurve(path, typeof(T), name, zero, zero);
        }
        
        public static SucroseAnimation WithBinaryCurve(this SucroseAnimation animation, Type type, string name, float zero)
        {
            return animation.WithBinaryCurve(string.Empty, type, name, zero, zero);
        }
        
        public static SucroseAnimation WithBinaryCurve<T>(this SucroseAnimation animation, string name, float zero)
        {
            return animation.WithBinaryCurve(string.Empty, typeof(T), name, zero, zero);
        }
    }
}