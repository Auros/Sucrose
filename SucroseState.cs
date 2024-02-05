using JetBrains.Annotations;
using UnityEditor.Animations;
using UnityEngine;

namespace Sucrose
{
    [PublicAPI]
    public class SucroseState : SucroseObject
    {
        private readonly SucroseLayer _layer;
        
        internal AnimatorState State { get; }
        
        public SucroseState(SucroseLayer layer, SucroseContainer sucrose) : base(sucrose)
        {
            _layer = layer;
            AnimatorState state = new();
            _layer.Layer.stateMachine.AddState(state, _layer.NextStatePosition());
            State = state;
        }

        public SucroseState WithName(string name)
        {
            State.name = name;
            return this;
        }

        public SucroseState WithMotion(Motion motion)
        {
            State.motion = motion;
            return this;
        }

        public SucroseState WithWriteDefaults(bool value)
        {
            State.writeDefaultValues = value;
            return this;
        }

        public SucroseTransition TransitionTo(SucroseState state)
        {
            SucroseTransition transition = new(this, state, _sucrose);
            return transition;
        }
    }
}