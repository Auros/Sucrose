using UnityEngine;

namespace Sucrose.Animation
{
    public class SucroseCurve
    {
        internal AnimationCurve Curve { get; }
        
        internal SucroseCurve()
        {
            Curve = new AnimationCurve();
        }

        public SucroseCurve AddKeyframe(float time, float value)
        {
            Curve.AddKey(time, value);
            return this;
        }
    }
}