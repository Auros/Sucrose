using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Sucrose
{
    [PublicAPI]
    public class SucroseParameter : SucroseObject
    {
        private readonly AnimatorControllerParameter _parameter;

        public string Name => _parameter.name;
        
        internal SucroseParameter(SucroseContainer sucrose) : base(sucrose)
        {
            _parameter = new AnimatorControllerParameter();
            _sucrose.Controller.AddParameter(_parameter);
        }

        public SucroseParameter WithName(string name)
        {
            var oldName = _parameter.name;
            _parameter.name = name;
            UpdateParameter(oldName);
            return this;
        }

        public SucroseParameter WithType(SucroseParameterType type)
        {
            var backingType = type switch
            {
                SucroseParameterType.Boolean => AnimatorControllerParameterType.Bool,
                SucroseParameterType.Integer => AnimatorControllerParameterType.Int,
                SucroseParameterType.Float => AnimatorControllerParameterType.Float,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            _parameter.type = backingType;
            UpdateParameter(_parameter.name);
            return this;
        }

        public SucroseParameter WithDefaultValue(bool value)
        {
            WithDefaultValue(value ? 1f : 0f);
            return this;
        }
        
        public SucroseParameter WithDefaultValue(float value)
        {
            switch (_parameter.type)
            {
                case AnimatorControllerParameterType.Bool:
                    _parameter.defaultBool = value >= 0.5f;
                    break;
                case AnimatorControllerParameterType.Int:
                    _parameter.defaultInt = (int)value;
                    break;
                case AnimatorControllerParameterType.Float:
                    _parameter.defaultFloat = value;
                    break;
                case AnimatorControllerParameterType.Trigger:
                    break;
                default:
                    throw new NotSupportedException("Trigger parameter types are not supported.");
            }
            UpdateParameter(_parameter.name);
            return this;
        }
        
        
        private void UpdateParameter(string name)
        {
            var parameters = _sucrose.Controller.parameters;
            var selfInController = parameters.FirstOrDefault(l => l.name == name);
            parameters[parameters.ToList().IndexOf(selfInController)] = _parameter;
            _sucrose.Controller.parameters = parameters;
        }
    }
}