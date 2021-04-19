using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class BulletsEmitterController : IUpdatable
    {
        private List<BulletController> _bullets = new List<BulletController>();
        private Transform _emitterTransform;

        private int _currentIndex;
        private float _timeTillNextBullet;

        private const float _delay = 2.0f;
        private const float _startSpeed = 9.0f;


        public BulletsEmitterController(List<LevelObjectView> bulletViews, Transform emitterTransform)
        {
            _emitterTransform = emitterTransform;

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
                _timeTillNextBullet = _delay;
                _bullets[_currentIndex].Throw(_emitterTransform.position, -_emitterTransform.up * _startSpeed);
                _currentIndex++;

                if (_currentIndex >= _bullets.Count)
                    _currentIndex = 0;
            }

            _bullets.ForEach(x => x.Update());
        }
    }
}
