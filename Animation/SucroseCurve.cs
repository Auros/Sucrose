using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

namespace Sucrose.Animation
{
    public class SucroseCurve : IDisposable
    {
        internal AnimationCurve Curve { get; }
        internal List<ObjectReferenceKeyframe> ObjectKeyframes { get; }
        
        internal SucroseCurve()
        {
            Curve = new AnimationCurve();
            ObjectKeyframes = ListPool<ObjectReferenceKeyframe>.Get(); 
            // should i use listpool here? seems like no point because this class shouldn't be processing a lot
        }

        public SucroseCurve AddKeyframe(float time, float value)
        {
            Curve.AddKey(time, value);
            return this;
        }

        public SucroseCurve AddObjectKeyframe(float time, UnityEngine.Object value)
        {
            ObjectKeyframes.Add(new()
            {
                time = time,
                value = value
            });
            return this;
        }

        public void Dispose()
        {
            ListPool<ObjectReferenceKeyframe>.Release(ObjectKeyframes);
        }
    }
}