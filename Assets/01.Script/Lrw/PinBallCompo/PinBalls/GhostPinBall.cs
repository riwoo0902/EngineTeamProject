using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo.PinBalls
{
    public class GhostPinBall : PinBallBase
    {
        [SerializeField] private float upPower = 5;
        
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
            Rigid.linearVelocityY = upPower;
            Rigid.linearVelocityX = 0;
        }
        
    }
}