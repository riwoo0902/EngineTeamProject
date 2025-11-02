using System.Collections.Generic;
using UnityEngine;

public abstract class GameAction
{
    public List<GameAction> PreReactions { get; private set; } = new(); // 사전 리액션
    public List<GameAction> PerformReactions { get; private set; } = new(); // 행동 중 리액션
    public List<GameAction> PostReactions { get; private set; } = new(); // 사후 리액션
}
