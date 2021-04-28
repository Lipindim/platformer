using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class BulletsEmitterController : IUpdatable
    {
        private readonly List<BulletController> _bullets = new List<BulletController>();
        private readonly Transform _emitterTransform;
        private readonly Transform _playerTransform;

        private int _currentIndex;
        private float _timeTillNextBullet;
        private float _squareAttackRange;

        private const float _delay = 2.0f;
        private const float _startSpeed = 10.0f;
        private const float _attackRange = 10.0f;


        public BulletsEmitterController(List<LevelObjectView> bulletViews, Transform emitterTransform, Transform playerTransform)
        {
            _emitterTransform = emitterTransform;
            _playerTransform = playerTransform;
            _squareAttackRange = _attackRange * _attackRange;

            foreach (LevelObjectView bulletView in bulletViews)
                _bullets.Add(new BulletController(bulletView));
        }

        public void Update()
        {
            if (_timeTillNextBullet > 0)
            {
                _bullets[_currentIndex].Active(false);
                _timeTillNextBullet -= Time.deltaTime;
            }
            else
            {
                Vector3 shootVector = _playerTransform.position - _emitterTransform.position;
                if (shootVector.sqrMagnitude < _squareAttackRange)
                {
                    Vector3 shootDirection = shootVector.normalized;
                    _timeTillNextBullet = _delay;
                    _bullets[_currentIndex].Throw(_emitterTransform.position, shootDirection * _startSpeed);
                    _currentIndex++;

                    if (_currentIndex >= _bullets.Count)
                        _currentIndex = 0;
                }
            }

        }
    }
}
