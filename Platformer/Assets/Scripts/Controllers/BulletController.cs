using UnityEngine;


namespace Platformer
{
    public class BulletController : IUpdatable
    {
        private float _radius = 0.13f;
        private Vector3 _velocity;

        private const float _groundLevel = -3.0f;
        private const float _g = -10.0f;
        private const float _attenuationCoefficient = 1.4f;

        private LevelObjectView _bulletView;
        private bool _startPosition;

        public BulletController(LevelObjectView bulletView)
        {
            _bulletView = bulletView;
            Active(false);
        }

        public void Update()
        {
            if (IsGrounded())
            {
                SetVelocity(_velocity.Change(newY: -_velocity.y / _attenuationCoefficient));
                _bulletView.transform.position = _bulletView.transform.position.Change(newY: _groundLevel + _radius);
            }
            else
            {
                SetVelocity(_velocity + Vector3.up * _g * Time.deltaTime);
                _bulletView.transform.position += _velocity * Time.deltaTime;
                if (_startPosition)
                    Active(true);
            }
        }

        public void Throw(Vector3 position, Vector3 velocity)
        {
            _bulletView.transform.position = position;
            SetVelocity(velocity);
            _startPosition = true;
        }

        public void Active(bool value)
        {
            _bulletView.TrailRenderer.enabled = value;
            _bulletView.gameObject.SetActive(value);
            _startPosition = false;
        }
        private void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            var angle = Vector3.Angle(Vector3.left, _velocity);
            var axis = Vector3.Cross(Vector3.left, _velocity);
            _bulletView.transform.rotation = Quaternion.AngleAxis(angle, axis);
        }

        private bool IsGrounded()
        {
            return _bulletView.transform.position.y <= _groundLevel + _radius + float.Epsilon && _velocity.y <= 0;
        }
    }
}
