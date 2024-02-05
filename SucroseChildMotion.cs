using UnityEditor.Animations;
using UnityEngine;

namespace Sucrose
{
    public class SucroseChildMotion : SucroseObject
    {
        private readonly int _index;
        
        public SucroseBlendTree BlendTree { get; }
        
        public SucroseChildMotion(SucroseBlendTree blendTree, SucroseContainer sucrose) : base(sucrose)
        {
            BlendTree = blendTree;
            _index = BlendTree.Tree.children.Length;
            BlendTree.Tree.AddChild(null!);
        }

        public SucroseChildMotion WithMotion(Motion motion)
        {
            var children = BlendTree.Tree.children;
            children[_index].motion = motion;
            BlendTree.Tree.children = children;
            return this;
        }

        public SucroseChildMotion WithThreshold(float value)
        {
            BlendTree.Tree.useAutomaticThresholds = false;
            var children = BlendTree.Tree.children;
            children[_index].threshold = value;
            BlendTree.Tree.children = children;
            
            return this;
        }

        public SucroseChildMotion WithDirectParameter(SucroseParameter parameter)
        {
            if (BlendTree.Tree is not { blendType: BlendTreeType.Direct })
                return this;
            
            var children = BlendTree.Tree.children;
            children[_index].directBlendParameter = parameter.Name;
            BlendTree.Tree.children = children;
            return this;
        }
    }
}