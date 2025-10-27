using System.Collections.Generic;
using Lrw_PinBall;
using UnityEngine;

public class ItemResner : MonoBehaviour
{
    [SerializeField] private GameObject spellUI;

    private ItemManager _spellManager;
    private List<GameObject> _spells = new List<GameObject>();

    private void Start()
    {
        _spellManager = ItemManager.Instance;

        _spellManager.AddSpell += AddSpellInUi;
        _spellManager.RemoveSpell += RemoveSpellInUi;
    }

    private void OnDestroy()
    {
        _spellManager.AddSpell -= AddSpellInUi;
        _spellManager.RemoveSpell -= RemoveSpellInUi;
    }

    private void AddSpellInUi(SpellDataSO spellSO)
    {
        GameObject spellObj = Instantiate(spellUI, transform);
        FireDataInItem itemData = spellObj.GetComponent<FireDataInItem>();
        itemData.NowSpell = spellSO;
        itemData.SetData();
        _spells.Add(spellObj);
    }

    private void RemoveSpellInUi(SpellDataSO spellSO)
    {
        for (int i = 0; i < _spells.Count; i++)
        {
            if (_spells[i].GetComponent<FireDataInItem>().NowSpell == spellSO)
            {
                GameObject spellObj = _spells[i];
                _spells.Remove(spellObj);
                Destroy(spellObj);
                break;
            }
        }
    }
}
