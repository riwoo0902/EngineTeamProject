using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo.PinBalls
{
    public class GhostPinBall : PinBallBase
    {
        [SerializeField] private LayerMask noCollisionMask;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            Physics.IgnoreLayerCollision(transform.gameObject.layer, noCollisionMask, Rigid.linearVelocityY > 0);
        }
    }
}