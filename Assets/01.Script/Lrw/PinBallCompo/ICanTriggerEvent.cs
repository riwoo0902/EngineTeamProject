namespace _01.Script.Lrw.PinBallCompo
{
    public interface ICanTriggerEvent
    {
        public float Score { get; set; }
        public float GetScore()
        {
            return Score;
        }
    }
}