using UnityEngine;


namespace Platformer
{

    public class CameraController : IUpdatable
    {
        private readonly Camera _camera;
        private readonly Transform _targetTransform;
        private readonly float _deltaX;
        private readonly float _deltaY;

        public CameraController(Transform targetTransform, Camera camera)
        {
            _camera = camera;
            _targetTransform = targetTransform;
            _deltaX = camera.transform.position.x - targetTransform.position.x;
            _deltaY = camera.transform.position.y - targetTransform.position.y;
        }

        public void Update()
        {
            _camera.transform.position = _camera.transform.position.Change(_targetTransform.position.x + _deltaX, _targetTransform.position.y + _deltaY);
        }
    }
}
