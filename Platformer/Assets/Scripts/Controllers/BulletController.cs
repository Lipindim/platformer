using UnityEngine;


namespace Platformer
{
    public class BulletController
    {
        private Vector3 _velocity;

        private LevelObjectView _bulletView;

        public BulletController(LevelObjectView bulletView)
        {
            _bulletView = bulletView;
            Active(false);
        }

        public void Throw(Vector3 position, Vector3 velocity)
        {
            Active(false);
            _bulletView.Rigidbody2D.velocity = Vector2.zero;
            _bulletView.Rigidbody2D.angularVelocity = 0;
            _bulletView.transform.position = position;
            Active(true);
            _bulletView.Rigidbody2D.AddForce(velocity, ForceMode2D.Impulse);

        }

        public void Active(bool value)
        {
            _bulletView.TrailRenderer.enabled = value;
            _bulletView.gameObject.SetActive(value);
        }

    }
}
