using UnityEngine;

namespace Game.Player
{
    public class PlayerRoad : MonoBehaviour
    {
        [SerializeField] private GameObject _road;

        [SerializeField] private SpherePlayer _spherePlayer;
        [SerializeField] private GameObject _endPoint;
        [SerializeField] private float _offset;

        private void Start()
        {
            UpdateRoad();
        }

        private void OnEnable()
        {
            _spherePlayer.UpdateSizePlayer += UpdateRoad;
            _spherePlayer.MovePlayer += UpdateRoad;
        }

        private void OnDisable()
        {
            _spherePlayer.UpdateSizePlayer -= UpdateRoad;
            _spherePlayer.MovePlayer -= UpdateRoad;
        }

        private void UpdateRoad()
        {
            CalculateRoadSize();
            CalculateRoadPosition();
        }
        
        private void CalculateRoadSize()
        {
            var distance = Vector3.Distance(_spherePlayer.transform.position, _endPoint.transform.position);
            _road.transform.localScale = new Vector3((distance / 10f) + _offset, 1f, _spherePlayer.SizePlayer / 10f);
        }

        private void CalculateRoadPosition()
        {
            float z = (_spherePlayer.transform.position.z + _endPoint.transform.position.z) / 2f;
            var position = _road.transform.position;
            position = new Vector3(position.x, position.y, z);
            _road.transform.position = position;
        }
    }
}