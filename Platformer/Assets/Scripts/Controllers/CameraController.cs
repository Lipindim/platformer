using UnityEngine;


namespace Platformer
{

    public class CameraController : IUpdatable
    {
        private readonly Camera _camera;
        private readonly Transform _targetTransform;
        private readonly float _deltaX;

        public CameraController(Transform targetTransform, Camera camera)
        {
            _camera = camera;
            _targetTransform = targetTransform;
            _deltaX = camera.transform.position.x - targetTransform.position.x;
        }

        public void Update()
        {
            _camera.transform.position = _camera.transform.position.Change(_targetTransform.position.x + _deltaX);
        }
    }
}
