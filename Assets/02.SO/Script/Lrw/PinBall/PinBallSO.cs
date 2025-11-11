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
        [field: SerializeField] public string BallName { get; private set; } = "Default";
        [field: SerializeField] public string BallExplanation { get; private set; } = "Notthing";
        [field: SerializeField] public float BallLinearDamping { get; private set; } = 0;
        [field: SerializeField] public float BallShootPower { get; private set; } = 5;
    }
    public enum BallType
    {
        Normal
    }
}

