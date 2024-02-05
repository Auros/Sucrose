using JetBrains.Annotations;
using UnityEditor.Animations;

namespace Sucrose
{
    [PublicAPI]
    public class SucroseBlendTree : SucroseObject
    {
        private readonly SucroseBlendTree? _parent;
        private readonly SucroseLayer _layer;
        private readonly BlendTree _blendTree;

        // ReSharper disable once ConvertToAutoPropertyWhenPossible
        internal BlendTree Tree => _blendTree;

        public SucroseBlendTree Parent => _parent ?? this;
        
        private int _index;
        
        public SucroseBlendTree(SucroseLayer layer, BlendTree blendTree, SucroseBlendTree parent, SucroseContainer sucrose) : base(sucrose)
        {
            _layer = layer;
            _parent = parent;
            _blendTree = blendTree;
            _index = parent.Tree.children.Length - 1;
            WithDirectParameter(_parent.Tree.blendParameter);
        }
        
        internal SucroseBlendTree(SucroseLayer layer, SucroseContainer sucrose) : base(sucrose)
        {
            _layer = layer;
            _sucrose.Controller.CreateBlendTreeInController("Sucrose Blend Tree", out var tree, _layer.GetIndex());
            _blendTree = tree;

            // Blend tree write defaults good
            foreach (var state in layer.Layer.stateMachine.states)
                state.state.writeDefaultValues = true;
        }
        
        public SucroseBlendTree WithName(string name)
        {
            _blendTree.name = name;
            return this;
        }

        public SucroseBlendTree WithType(BlendTreeType type)
        {
            _blendTree.blendType = type;
            return this;
        }

        public SucroseBlendTree WithParameter(SucroseParameter parameter)
        {
            _blendTree.blendParameter = parameter.Name;
            _blendTree.blendParameterY = parameter.Name;
            return this;
        }

        public SucroseBlendTree WithDirectParameter(SucroseParameter parameter)
        {
            return WithDirectParameter(parameter.Name);
        }

        public SucroseBlendTree NewChildBlendTree()
        {
            var tree = _blendTree.CreateBlendTreeChild(0);
            return new SucroseBlendTree(_layer, tree, this, _sucrose);
        }

        public SucroseChildMotion NewChildMotion()
        {
            SucroseChildMotion motion = new(this, _sucrose);
            return motion;
        }
        
        private SucroseBlendTree WithDirectParameter(string parameterName)
        {
            if (_parent is not { Tree: { blendType: BlendTreeType.Direct } })
                return this;
            
            var children = _parent.Tree.children;
            children[_index].directBlendParameter = parameterName;
            _parent.Tree.children = children;
            
            return this;
        }

    }
}