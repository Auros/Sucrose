﻿using System.Linq;
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

        public SucroseState Exit()
        {
            var exit = State.AddExitTransition();
            exit.hasExitTime = true;
            exit.exitTime = 0.01f;
            return this;
        }

        public T GetStateMachineBehaviour<T>() where T : StateMachineBehaviour
        {
            var behaviour = State.behaviours.FirstOrDefault(b => b.GetType() == typeof(T));
            if (behaviour == null)
                behaviour = State.AddStateMachineBehaviour<T>();
            return (T)behaviour;
        }

        public SucroseTransition TransitionTo(SucroseState state)
        {
            SucroseTransition transition = new(this, state, _sucrose);
            return transition;
        }
    }
}