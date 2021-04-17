using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private int _animationSpeed = 10;
        [SerializeField] private LevelObjectView _playerView;

        private SpriteAnimationConfig _playerConfig;
        private List<IUpdatable> _updatables = new List<IUpdatable>();

        private void Awake()
        {
            _playerConfig = Resources.Load<SpriteAnimationConfig>("AnimationPlayerConfig");
            SpriteAnimatorController spriteAnimatorController = new SpriteAnimatorController(_playerConfig);
            _updatables.Add(spriteAnimatorController);
            spriteAnimatorController.StartAnimation(_playerView.SpriteRenderer, AnimationState.Run, true, _animationSpeed);
        }

        private void Update()
        {
            foreach (var updatable in _updatables)
            {
                updatable.Update();
            }
        }

        private void FixedUpdate()
        {

        }
    }
}
