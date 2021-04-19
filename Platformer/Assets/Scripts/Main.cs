using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private CannonView _cannonView;

        private SpriteAnimationConfig _playerConfig;
        private List<IUpdatable> _updatables = new List<IUpdatable>();

        private void Awake()
        {
            _playerConfig = Resources.Load<SpriteAnimationConfig>("AnimationPlayerConfig");
            var spriteAnimatorController = new SpriteAnimatorController(_playerConfig);
            var playerMoveController = new PlayerMoveController(_playerView.transform, _playerView.SpriteRenderer, spriteAnimatorController);
            var cannonAimController = new CannonAimController(_cannonView.BarrelTransform, _playerView.transform);
            var bulletsEmitterController = new BulletsEmitterController(_cannonView.Bullets, _cannonView.EmitterTransform);
            _updatables.Add(spriteAnimatorController);
            _updatables.Add(playerMoveController);
            _updatables.Add(cannonAimController);
            _updatables.Add(bulletsEmitterController);
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
