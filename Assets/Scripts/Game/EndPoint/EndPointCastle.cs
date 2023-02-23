using Game.Player;
using UnityEngine;

namespace Game.EndPoint
{
    public class EndPointCastle : MonoBehaviour, IEndPoint
    {
        [SerializeField] private Animation _animation;

        private bool _openDoor;
        
        private void OnTriggerEnter(Collider other)
        {
            if (_openDoor) 
                return;
            
            if (other.TryGetComponent(out IPlayer player))
            {
                _animation.Play();
                _openDoor = true;
            }
        }
    }
}
