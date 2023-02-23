using UnityEngine;

namespace Game.Player
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private SpherePlayer _player;
        [SerializeField] private Vector3 _offset;
        
        private void Update()
        {
            transform.position = _player.transform.position + _offset;
        }
    }
}