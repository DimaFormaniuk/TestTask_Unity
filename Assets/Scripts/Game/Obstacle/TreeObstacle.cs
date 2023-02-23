using System.Collections;
using UnityEngine;

namespace Game.Obstacle
{
    public class TreeObstacle : MonoBehaviour, IObstacle
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _timeDestroy = 2f;
        
        public void Explosion(float bounceForce, Vector3 vectorMove)
        {
            _rigidbody.AddExplosionForce(bounceForce, transform.position + ExplosionPosition(), 1f);

            StartCoroutine(DestroyTimer());
        }

        private Vector3 ExplosionPosition() =>
            transform.position.x switch
            {
                < 0 => new Vector3(1, 0, 0),
                > 0 => new Vector3(-1, 0, 0),
                0 => new Vector3(Random.Range(0, 100) < 50 ? -1 : 1, 0, 0),
                _ => new Vector3(0,0,0)
            };

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSecondsRealtime(_timeDestroy);
            Destroy(gameObject);
        }
    }
}