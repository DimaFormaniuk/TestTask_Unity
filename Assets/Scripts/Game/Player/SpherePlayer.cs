using System;
using System.Collections;
using Game.EndPoint;
using Game.Obstacle;
using Game.UI;
using UnityEngine;

namespace Game.Player
{
    public class SpherePlayer : MonoBehaviour, IPlayer
    {
        public event Action UpdateSizePlayer;
        public event Action MovePlayer;
        
        [SerializeField] private GameObject _player;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _minSize = 0.2f;
        [SerializeField] private UIGame _uiGame;
        
        private Vector3 _vectorMove;
        private Coroutine _coroutine;

        public float SizePlayer => _player.transform.localScale.x;
        public bool CanDecrement(float value) => SizePlayer - value > _minSize;

        public void Init(Vector3 vectorMove)
        {
            _vectorMove = vectorMove;
        }

        public void DecrementSize(float value)
        {
            var tempSize = _player.transform.localScale.x - value;
            _player.transform.localScale = new Vector3(tempSize, tempSize, tempSize);

            CalculateY();
            UpdateSizePlayer?.Invoke();
        }

        public void StopMove()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        public void StartMove()
        {
            StopMove();
            _coroutine = StartCoroutine(Move());
        }

        private void CalculateY()
        {
            var position = _player.transform.position;
            _player.transform.position =
                new Vector3(position.x, (_player.transform.localScale.x / 2f) + 0.02f, position.z);
        }

        private IEnumerator Move()
        {
            while (true)
            {
                transform.Translate(_vectorMove * (Time.fixedDeltaTime * _speed));
                MovePlayer?.Invoke();
                yield return null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IEndPoint endPoint))
            {
                Win();
                StopMove();
            }

            if (other.TryGetComponent(out IObstacle obstacle))
            {
                Lose();
                StopMove();
            }
        }

        private void Win()
        {
            _uiGame.ShowWindow(WindowType.Win);
            Debug.LogError("Win");
        }

        private void Lose()
        {
            _uiGame.ShowWindow(WindowType.Lose);
            Debug.LogError("Lose");
        }
    }
}