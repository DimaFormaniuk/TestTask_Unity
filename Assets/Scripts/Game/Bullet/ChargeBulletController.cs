using System.Collections;
using Game.Input;
using Game.Player;
using UnityEngine;

namespace Game.Bullet
{
    public class ChargeBulletController : MonoBehaviour
    {
        [SerializeField] private UserInput _userInput;

        [SerializeField] private SpherePlayer _player;
        [SerializeField] private GameObject EndPoint;

        [SerializeField] private SphereBullet pf_Bullet;
        [SerializeField] private GameObject _parentBullet;

        [SerializeField]private float _stepSize = 0.001f;

        private SphereBullet _bullet;
        private Coroutine _coroutine;

        private void Start()
        {
            _player.Init(VectorRoad());
        }

        private void OnEnable()
        {
            _userInput.ClickFire += OnClickFire;
        }

        private void OnDisable()
        {
            _userInput.ClickFire -= OnClickFire;
        }

        private void OnClickFire(bool value)
        {
            if (value)
            {
                if (CheckCanCreateBullet())
                {
                    _player.StopMove();
                    CreateBullet();
                    _coroutine = StartCoroutine(ChargeBullet());
                }
            }
            else
            {
                _player.StartMove();
                if (_coroutine != null)
                    StopCoroutine(_coroutine);

                if (_bullet != null)
                {
                    _bullet.Shot();
                    _bullet = null;
                }
            }
        }

        private bool CheckCanCreateBullet()
        {
            return _player.CanDecrement(pf_Bullet.StartSizeBullet);
        }

        private void CreateBullet()
        {
            _bullet = Instantiate(pf_Bullet, _parentBullet.transform);
            Vector3 position = _player.transform.position;
            _bullet.transform.position = new Vector3(position.x, 0f, position.z);
            Vector3 vectorRoad = VectorRoad();
            _bullet.Init(vectorRoad);
            _bullet.transform.Translate(vectorRoad * _player.transform.localScale.x);
        }

        private Vector3 VectorRoad()
        {
            Vector3 position = _player.transform.position;
            Vector3 temp = EndPoint.transform.position;
            temp.y = position.y;
            temp -= position;
            temp.Normalize();
            return temp;
        }

        private IEnumerator ChargeBullet()
        {
            _player.DecrementSize(_bullet.StartSizeBullet);

            while (true)
            {
                if (_player.CanDecrement(_stepSize) == false)
                    break;

                _bullet?.IncrementSize(_stepSize);
                _player.DecrementSize(_stepSize);

                yield return null;
            }
        }
    }
}