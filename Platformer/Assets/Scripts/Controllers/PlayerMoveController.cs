using UnityEngine;


namespace Platformer
{
    public class PlayerMoveController : IUpdatable
    {
        private readonly SpriteRenderer _playerSpriteRenderer;
        private readonly Transform _playerTransform;
        private readonly SpriteAnimatorController _playerSpriteAnimatorController;

        private const float _moveSpeed = 5.0f;
        private const float _animationSpeed = 10.0f;
        private const float _groundLevel = -2.0f;
        private const float _jumpStartSpeed = 8.0f;
        private const float _g = -10.0f;
        
        private bool _doJump;
        private float _xMove;
        private float _yVelocity;

        public PlayerMoveController(Transform playerTransform, SpriteRenderer playerSpriteRenderer, SpriteAnimatorController playerSpriteAnimatorController)
        {
            _playerSpriteRenderer = playerSpriteRenderer;
            _playerTransform = playerTransform;
            _playerSpriteAnimatorController = playerSpriteAnimatorController;
        }

        public void Update()
        {
            _doJump = Input.GetAxis("Vertical") > 0;
            _xMove = Input.GetAxis("Horizontal");

            if (_xMove == 0)
                _playerSpriteAnimatorController.StartAnimation(_playerSpriteRenderer, AnimationState.Idle, true, _animationSpeed);
            else
                GoSideWay();

            if (IsGrounded())
            {
                if (_doJump && _yVelocity == 0)
                {
                    _yVelocity = _jumpStartSpeed;
                }
                else if (_yVelocity < 0)
                {
                    _yVelocity = 0;
                    _playerTransform.position = _playerTransform.position.Change(newY: _groundLevel);
                }
            }
            else
            {
                if (_yVelocity > 0)
                    _playerSpriteAnimatorController.StartAnimation(_playerSpriteRenderer, AnimationState.JumpUp, true, _animationSpeed);
                else if (_yVelocity < 0)
                    _playerSpriteAnimatorController.StartAnimation(_playerSpriteRenderer, AnimationState.JumpDown, true, _animationSpeed);

                _yVelocity += _g * Time.deltaTime;
                _playerTransform.position += Vector3.up * (Time.deltaTime * _yVelocity);
            }

            
        }

        private void GoSideWay()
        {
            float direction = _xMove > 0 ? 1 : -1;
            _playerTransform.localScale = _playerTransform.localScale.Change(direction);
            _playerTransform.position = _playerTransform.position.Update(Time.deltaTime * direction * _moveSpeed);
            _playerSpriteAnimatorController.StartAnimation(_playerSpriteRenderer, AnimationState.Run, true, _animationSpeed);
        }

        private bool IsGrounded()
        {
            return _playerTransform.position.y <= _groundLevel + float.Epsilon && _yVelocity <= 0;
        }

    }
}
