using System;
using UnityEngine;


namespace Platformer
{
    public class PlayerHealthController
    {
        public event Action OnDie;

        private readonly LevelObjectView _playerView;

        private float _health = 3.0f;


        public PlayerHealthController(LevelObjectView playerView)
        {
            _playerView = playerView;
            _playerView.OnCollisionEnter += PlayerViewOnCollision;
            _playerView.OnTriggerEnter += PlayerViewOnTriggerEnter;
        }

        private void PlayerViewOnTriggerEnter(LevelObjectView triggerObject)
        {
            if (triggerObject.tag == "DeathZone")
            {
                _health = 0;
                OnDie?.Invoke();
            }
        }

        private void PlayerViewOnCollision(LevelObjectView collisionObject)
        {
            if (collisionObject.tag == "Bullet")
            {
                _health--;
                collisionObject.gameObject.SetActive(false);
                if (_health <= 0)
                    OnDie?.Invoke();
            }
        }

        ~PlayerHealthController()
        {
            _playerView.OnCollisionEnter -= PlayerViewOnCollision;
            _playerView.OnTriggerEnter -= PlayerViewOnTriggerEnter;
        }
    }
}
