using System;
using System.Collections.Generic;
using Lrw_PinBall;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public Action<SpellDataSO> OnChagedSpell;
    public Action<SpellDataSO> AddSpell;
    public Action<SpellDataSO> RemoveSpell;

    public List<SpellDataSO> testSOs = new List<SpellDataSO>();
    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [ContextMenu("Fire")]
    private void FireTest()
    {
        for (int i = 0; i < testSOs.Count; i++)
        {
            AddSpell?.Invoke(testSOs[i]);
        }
    }

    [ContextMenu("Remove")]
    private void RemoveTest()
    {
        for (int i = 0; i < testSOs.Count; i++)
        {
            RemoveSpell?.Invoke(testSOs[i]);
        }
    }

    public void ChangeNowPinBall(SpellDataSO targetSpell)
    {
        OnChagedSpell?.Invoke(targetSpell);
    }
}
