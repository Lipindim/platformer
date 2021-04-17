using System;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{
    [CreateAssetMenu(fileName = "AnimationConfig", menuName = "configs/sprite-animation")]
    public class SpriteAnimationConfig : ScriptableObject
    {
        public List<AnimationSequence> Sequences;


        [Serializable]
        public sealed class AnimationSequence
        {
            public AnimationState Track;
            public List<Sprite> Sprites;
        }
    }
}
