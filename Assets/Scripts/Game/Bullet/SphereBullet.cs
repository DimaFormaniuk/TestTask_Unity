using System.Collections;
using Game.EndPoint;
using Game.Obstacle;
using UnityEngine;

namespace Game.Bullet
{
    public class SphereBullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _startSizeBullet;
        [SerializeField] private float _bounceForce;
        
        [SerializeField] private GameObject _bullet;
        [SerializeField] private RadiusDamage _radiusDamage;
        [SerializeField] private BoxCollider _boxCollider;

        public float StartSizeBullet => _startSizeBullet;

        private Vector3 _vectorMove;

        public void Init(Vector3 vectorRoad)
        {
            _vectorMove = vectorRoad;

            _radiusDamage.Init(0.5f);
            _radiusDamage.Show();
        }

        public void IncrementSize(float value)
        {
            var tempSize = _bullet.transform.localScale.x + value;
            _bullet.transform.localScale = new Vector3(tempSize, tempSize, tempSize);

            CalculateY();
            _radiusDamage.CalculateRadius(tempSize);
        }

        public void Shot()
        {
            //_radiusDamage.Hide();
            _bullet = null;
            CalculateCollider();
            StartCoroutine(Move());
        }

        private void CalculateCollider()
        {
            _boxCollider.size = new Vector3(_radiusDamage.BounceRadius, 1f, _radiusDamage.BounceRadius);
        }

        private void CalculateY()
        {
            var position = _bullet.transform.position;
            _bullet.transform.position =
                new Vector3(position.x, _bullet.transform.localScale.x / 2f, position.z);
        }

        private IEnumerator Move()
        {
            while (true)
            {
                transform.Translate(_vectorMove * (Time.fixedDeltaTime * _speed));
                yield return null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IObstacle obstacle))
            {
                Explosion();
                Destroy(gameObject);
            }

            if (other.TryGetComponent(out IEndPoint endPoint))
            {
                Destroy(gameObject);
            }
        }

        private void Explosion()
        {
            Collider[] overlapped = Physics.OverlapSphere(transform.position, _radiusDamage.BounceRadius * 10f);

            float bounceForce = _radiusDamage.BounceRadius * _bounceForce;
            
            for (int i = 0; i < overlapped.Length; i++)
                if (overlapped[i].TryGetComponent(out IObstacle obstacle))
                    obstacle.Explosion(bounceForce, _vectorMove);
        }
    }
}