namespace _01.Script.Lrw.PinBallCompo
{
    public interface ICanTriggerEvent
    {
        public int Score { get; set; }
        public int GetScore()
        {
            return Score;
        }
    }
}