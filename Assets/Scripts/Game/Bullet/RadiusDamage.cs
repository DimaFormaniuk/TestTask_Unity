using UnityEngine;

namespace Game.Bullet
{
    public class RadiusDamage : MonoBehaviour
    {
        [SerializeField] private GameObject _radius;

        private float _multiplierSize = 1f;
        private float _bounceRadius;
        public float BounceRadius => _bounceRadius;

        public void Init(float multiplierSize)
        {
            _multiplierSize = multiplierSize;
        }
        
        public void Show()
        {
            _radius.SetActive(true);
        }

        public void Hide()
        {
            _radius.SetActive(false);
        }

        public void CalculateRadius(float value)
        {
            _bounceRadius = value * _multiplierSize;
            _radius.transform.localScale = new Vector3(_bounceRadius, _bounceRadius, _bounceRadius);
        }
    }
}
