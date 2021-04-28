using UnityEngine.SceneManagement;


namespace Platformer
{
    public class GameController
    {
        private readonly PlayerHealthController _playerHealthController;

        public GameController(PlayerHealthController playerHealthController)
        {
            _playerHealthController = playerHealthController;
            _playerHealthController.OnDie += PlayerHealthControllerOnDie;
        }

        private void PlayerHealthControllerOnDie()
        {
            SceneManager.LoadScene(0);
        }

        ~GameController()
        {
            _playerHealthController.OnDie -= PlayerHealthControllerOnDie;
        }
    }
}
