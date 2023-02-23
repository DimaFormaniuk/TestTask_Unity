using UnityEngine;

namespace Game.Obstacle
{
    public interface IObstacle
    {
        void Explosion(float bounceForce, Vector3 vectorMove);
    }
}