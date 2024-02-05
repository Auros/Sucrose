using Sucrose.Animation;
using UnityEditor.Animations;

namespace Sucrose
{
    public class SucroseContainer
    {
        internal AnimatorController Controller { get; }

        public int LayerCount => Controller.layers.Length;

        public SucroseContainer(AnimatorController animatorController)
        {
            Controller = animatorController;
        }

        public SucroseLayer NewLayer()
        {
            SucroseLayer layer = new(this);
            return layer;
        }

        public SucroseParameter NewParameter()
        {
            SucroseParameter parameter = new(this);
            return parameter;
        }

        public SucroseAnimation NewAnimation(SucroseAnimationBuilder builder)
        {
            SucroseAnimation animation = new(this);
            builder.Invoke(animation);
            return animation;
        }
    }
}