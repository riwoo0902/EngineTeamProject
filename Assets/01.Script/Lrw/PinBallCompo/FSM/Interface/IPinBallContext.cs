using UnityEngine;
namespace _01.Script.Lrw.PinBallCompo.FSM.Interface
{
    public interface IPinBallContext
    {
        public Transform PinBallContextTransform { get; }
        public Rigidbody2D PinBallContextRigidbody { get; }
    }
}