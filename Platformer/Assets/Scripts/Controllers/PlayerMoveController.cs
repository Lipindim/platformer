using Assets.Scripts.Extensions;
using UnityEngine;


namespace Platformer
{
    public class PlayerMoveController : IUpdatable
    {
        private readonly SpriteRenderer _playerSpriteRenderer;
        private readonly Transform _playerTransform;
        private readonly SpriteAnimatorController _playerSpriteAnimatorController;
        private readonly ContactsPoller _contactsPoller;
        private readonly Rigidbody2D _playerRigidbody2D;

        private const float _moveSpeed = 5.0f;
        private const float _animationSpeed = 10.0f;
        private const float _jumpForce = 8.0f;
        
        private bool _doJump;
        private float _xMove;

        public PlayerMoveController(LevelObjectView playerView, SpriteAnimatorController playerSpriteAnimatorController)
        {
            _playerSpriteRenderer = playerView.SpriteRenderer;
            _playerTransform = playerView.Transform;
            _playerSpriteAnimatorController = playerSpriteAnimatorController;
            _playerRigidbody2D = playerView.Rigidbody2D;
            _contactsPoller = new ContactsPoller(playerView.Collider2D);
        }

        public void Update()
        {
            _contactsPoller.Update();

            _doJump = Input.GetAxis("Vertical") > 0;
            _xMove = Input.GetAxis("Horizontal");

            if (_xMove == 0)
                _playerSpriteAnimatorController.StartAnimation(_playerSpriteRenderer, AnimationState.Idle, true, _animationSpeed);
            else
                GoSideWay();

            if (_contactsPoller.IsGrounded && _playerRigidbody2D.velocity.y == 0)
            {
                if (_doJump)
                    _playerRigidbody2D.AddForce(_playerTransform.up * _jumpForce, ForceMode2D.Impulse);
            }
            else
            {
                if (_playerRigidbody2D.velocity.y > 0)
                    _playerSpriteAnimatorController.StartAnimation(_playerSpriteRenderer, AnimationState.JumpUp, true, _animationSpeed);
                else if (_playerRigidbody2D.velocity.y < 0)
                    _playerSpriteAnimatorController.StartAnimation(_playerSpriteRenderer, AnimationState.JumpDown, true, _animationSpeed);
            }


        }

        private void GoSideWay()
        {
            float direction = _xMove > 0 ? 1 : -1;
            if (direction > 0 && !_contactsPoller.HasRightContacts || direction < 0 && !_contactsPoller.HasLeftContacts)
            {
                _playerTransform.localScale = _playerTransform.localScale.Change(direction);
                _playerRigidbody2D.velocity = _playerRigidbody2D.velocity.Change(newX: _moveSpeed * direction);
                _playerSpriteAnimatorController.StartAnimation(_playerSpriteRenderer, AnimationState.Run, true, _animationSpeed);
            }
        }

    }
}
