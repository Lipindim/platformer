using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Platformer
{
    public class CoinsController : IDisposable
    {
        private readonly List<LevelObjectView> _coinsView;
        private readonly SpriteAnimatorController _spriteAnimatorController;
        private readonly LevelObjectView _playerView;
        private float _animationSpeed = 10.0f;

        public CoinsController(List<LevelObjectView> coinsView, SpriteAnimatorController spriteAnimatorController, LevelObjectView playerView)
        {
            _coinsView = coinsView;
            _spriteAnimatorController = spriteAnimatorController;
            _playerView = playerView;
            _playerView.OnTriggerEnter += PlayerViewOnCollision;

            foreach (var coinView in _coinsView)
            {
                _spriteAnimatorController.StartAnimation(coinView.SpriteRenderer, AnimationState.Run, true, _animationSpeed);
            }
        }

        private void PlayerViewOnCollision(LevelObjectView collisionView)
        {
            if (_coinsView.Contains(collisionView))
            {
                _spriteAnimatorController.StopAnimation(collisionView.SpriteRenderer);
                GameObject.Destroy(collisionView.gameObject);
            }
        }

        public void Dispose()
        {
            _playerView.OnTriggerEnter -= PlayerViewOnCollision;
        }
    }
}
