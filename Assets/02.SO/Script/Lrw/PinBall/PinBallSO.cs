using UnityEngine;

namespace Lrw_PinBall
{
    [CreateAssetMenu(fileName = "PinBallSO", menuName = "LrwSO/PinBallSO")]
    public class PinBallSO : ScriptableObject
    {
        [field: SerializeField] public BallType PinBallType { get; private set; } = BallType.Normal;
        [field: SerializeField] public Sprite PinBallImage { get; private set; }
        [field: SerializeField] public float Mass { get; private set; } = 1;
        [field: SerializeField] public float Friction { get; private set; } = 0.2f;
        [field: SerializeField] public float Bounciness { get; private set; } = 0.5f;

        [field: SerializeField] public float BaseDamage { get; private set; } = 1;


    }
    public enum BallType
    {
        Normal
    }
}

