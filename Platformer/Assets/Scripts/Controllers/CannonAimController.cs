using UnityEngine;


namespace Platformer
{
    public class CannonAimController : IUpdatable
    {
        private readonly Transform _barrelTransform;
        private readonly Transform _aimTransform;
        
        private Vector3 _direction;
        private float _angle;
        private Vector3 _axis;

        public CannonAimController(Transform barrelTransform, Transform aimTransform)
        {
            _barrelTransform = barrelTransform;
            _aimTransform = aimTransform;
        }
        public void Update()
        {
            _direction = _aimTransform.position - _barrelTransform.position;
            _angle = Vector3.Angle(Vector3.down, _direction);
            _axis = Vector3.Cross(Vector3.down, _direction);
            _barrelTransform.rotation = Quaternion.AngleAxis(_angle, _axis);
        }
    }
}
