using System;
using UnityEngine;

[Serializable]
public class EnemySlot
{
    public Transform Pos;
    public Enemy CurUse;

    public bool IsUse => CurUse != null; //칸에 요소가 있다면 True
}

