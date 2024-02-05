using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace Sucrose
{
    [PublicAPI]
    public class SucroseLayer : SucroseObject
    {
        private Vector2 _cursor = new(200f, 0f);
        
        internal AnimatorControllerLayer Layer { get; private set; }
        
        internal SucroseLayer(SucroseContainer sucrose) : base(sucrose)
        {
            Layer = new AnimatorControllerLayer
            {
                name = _sucrose.Controller.MakeUniqueLayerName(Guid.NewGuid().ToString()),
                stateMachine = new AnimatorStateMachine(),
                defaultWeight = 1f,
            };
            sucrose.Controller.AddLayer(Layer);
        }

        public SucroseLayer WithName(string name)
        {
            var oldName = Layer.name;
            Layer.name = name;
            UpdateLayer(oldName);
            return this;
        }

        public SucroseLayer WithWeight(float weight)
        {
            Layer.defaultWeight = weight;
            return this;
        }

        public SucroseState NewState()
        {
            SucroseState state = new(this, _sucrose);
            return state;
        }

        public SucroseBlendTree NewBlendTree()
        {
            SucroseBlendTree blendTree = new(this, _sucrose);
            return blendTree;
        }

        private void UpdateLayer(string name)
        {
            var layers = _sucrose.Controller.layers;
            var selfInController = layers.FirstOrDefault(l => l.name == name);
            layers[layers.ToList().IndexOf(selfInController)] = Layer;
            _sucrose.Controller.layers = layers;
        }

        internal Vector2 NextStatePosition()
        {
            _cursor = new Vector2(_cursor.x, _cursor.y + 80f);
            return _cursor;
        }

        internal int GetIndex()
        {
            var layers = _sucrose.Controller.layers;
            var selfInController = layers.FirstOrDefault(l => l.name == Layer.name);
            return layers.ToList().IndexOf(selfInController);
        }
    }
}