using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Sucrose.Animation
{
    [PublicAPI]
    public class SucroseAnimation
    {
        private readonly AnimationClip _clip;
        private readonly SucroseContainer _sucrose;

        // ReSharper disable once ConvertToAutoPropertyWhenPossible
        internal AnimationClip Animation => _clip;
        
        internal SucroseAnimation(SucroseContainer sucrose)
        {
            _sucrose = sucrose;
            _clip = new AnimationClip();
            
            if (EditorUtility.IsPersistent(sucrose.Controller))
                AssetDatabase.AddObjectToAsset(_clip, sucrose.Controller);
        }

        public SucroseAnimation WithName(string name)
        {
            _clip.name = name;
            return this;
        }

        public SucroseAnimation WithCurve(string path, Type type, string name, SucroseCurveBuilder builder)
        {
            using (SucroseCurve curve = new())
            {
                builder.Invoke(curve);

                _clip.SetCurve(path, type, name, curve.Curve);
                if (curve.ObjectKeyframes.Count > 0)
                {
                    var binding = EditorCurveBinding.PPtrCurve(path, type, name);
                    AnimationUtility.SetObjectReferenceCurve(_clip, binding, curve.ObjectKeyframes.ToArray());
                }
            }
            return this;
        }

        public SucroseAnimation WithWrapMode(WrapMode wrapMode)
        {
            _clip.wrapMode = wrapMode;
            return this;
        }
    }
}