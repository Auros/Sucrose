using UnityEditor.Animations;

namespace Sucrose
{
    public class SucroseTransition : SucroseObject
    {
        private readonly SucroseState _source;
        private readonly SucroseState _destination;
        private readonly AnimatorStateTransition _transition;
        
        internal SucroseTransition(SucroseState source, SucroseState destination, SucroseContainer sucrose) : base(sucrose)
        {
            _source = source;
            _destination = destination;
            _transition = _source.State.AddTransition(_destination.State);
            _transition.hasFixedDuration = true;
            _transition.hasExitTime = false;
            _transition.exitTime = 0f;
            _transition.duration = 0f;
        }

        // ReSharper disable once ConvertToAutoPropertyWhenPossible
        public SucroseState Source => _source;

        // ReSharper disable once ConvertToAutoPropertyWhenPossible
        public SucroseState Destination => _destination;

        public SucroseTransition WithCondition(string parameterName, AnimatorConditionMode mode, float threshold)
        {
            _transition.AddCondition(mode, threshold, parameterName);
            return this;
        }
        
        public SucroseTransition WithCondition(SucroseParameter parameter, AnimatorConditionMode mode, float threshold)
        {
            return WithCondition(parameter.Name, mode, threshold);
        }
    }
}