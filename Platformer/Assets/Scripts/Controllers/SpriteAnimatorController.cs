using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class SpriteAnimatorController : IUpdatable, IDisposable
    {
        private readonly SpriteAnimationConfig _config;
        private readonly Dictionary<SpriteRenderer, Animation> _activeAnimations = new Dictionary<SpriteRenderer, Animation>();

        private sealed class Animation : IUpdatable
        {
            public List<Sprite> Sprites;
            public float Counter;
            public float Speed = 10;
            public AnimationState Track;
            public bool Sleeps;
            public bool Loop = true;

            public void Update()
            {
                if (Sleeps)
                    return;

                Counter += Time.deltaTime * Speed;
                if (Loop)
                {
                    while (Counter > Sprites.Count)
                        Counter -= Sprites.Count;
                }
                else if (Counter > Sprites.Count)
                {
                    Counter = Sprites.Count;
                    Sleeps = true;
                }
            }
        }


        public SpriteAnimatorController(SpriteAnimationConfig spriteAnimationConfig)
        {
            _config = spriteAnimationConfig;
        }

        public void StartAnimation(SpriteRenderer spriteRenderer, AnimationState track, bool loop, float speed)
        {
            if (!_activeAnimations.TryGetValue(spriteRenderer, out Animation animation))
            {
                animation = new Animation();
                _activeAnimations.Add(spriteRenderer, animation);
            }

            animation.Loop = loop;
            animation.Speed = speed;
            animation.Sleeps = false;
            if (animation.Track != track)
            {
                animation.Track = track;
                animation.Sprites = _config.Sequences.Find(x => x.Track == track).Sprites;
                animation.Counter = 0;
            }
        }

        public void StopAnimation(SpriteRenderer spriteRenderer)
        {
            if (_activeAnimations.ContainsKey(spriteRenderer))
                _activeAnimations.Remove(spriteRenderer);
        }

        public void Dispose()
        {
            _activeAnimations.Clear();
        }

        public void Update()
        {
            foreach (var animation in _activeAnimations)
            {
                animation.Value.Update();
                if (animation.Value.Counter < animation.Value.Sprites.Count)
                    animation.Key.sprite = animation.Value.Sprites[(int)animation.Value.Counter];
            }
        }
    }
}
