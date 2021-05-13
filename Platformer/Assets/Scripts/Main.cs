using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private CannonView _cannonView;
        [SerializeField] private List<LevelObjectView> _coins;
        [SerializeField] private GenerateLevelView _generateLevelView;
        [SerializeField] private QuestStoryConfig[] _questStoryConfigs;

        private SpriteAnimationConfig _playerConfig;
        private SpriteAnimationConfig _coinConfig;
        private List<IUpdatable> _updatables = new List<IUpdatable>();
        private List<IUpdatable> _fixedUpdatables = new List<IUpdatable>();

        private void Awake()
        {
            _playerConfig = Resources.Load<SpriteAnimationConfig>("AnimationPlayerConfig");
            _coinConfig = Resources.Load<SpriteAnimationConfig>("AnimationCoinConfig");
            var spriteAnimatorController = new SpriteAnimatorController(_playerConfig);
            var spriteAnimatorCoinController = new SpriteAnimatorController(_coinConfig);
            var playerMoveController = new PlayerMoveController(_playerView, spriteAnimatorController);
            var cannonAimController = new CannonAimController(_cannonView.BarrelTransform, _playerView.transform);
            var bulletsEmitterController = new BulletsEmitterController(_cannonView.Bullets, _cannonView.EmitterTransform, _playerView.Transform);
            var coinController = new CoinsController(_coins, spriteAnimatorCoinController, _playerView);
            var playerHealthController = new PlayerHealthController(_playerView);
            var gameController = new GameController(playerHealthController);
            var questConfigurator = new QuestsConfigurator(_questStoryConfigs);
            var cameraController = new CameraController(_playerView.Transform, Camera.main);
            //var generateLevelController = new GeneratorLevelController(_generateLevelView);
            //generateLevelController.Awake();
            _updatables.Add(spriteAnimatorController);
            _fixedUpdatables.Add(playerMoveController);
            _updatables.Add(cannonAimController);
            _updatables.Add(bulletsEmitterController);
            _updatables.Add(spriteAnimatorCoinController);
            _updatables.Add(cameraController);
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
            foreach (var updatable in _fixedUpdatables)
            {
                updatable.Update();
            }
        }
    }
}
