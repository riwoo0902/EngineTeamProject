using Lrw_PinBall;
using TMPro;
using UnityEngine;

public class FireDataInItem : MonoBehaviour
{
    public SpellDataSO NowSpell;

    [SerializeField] private TMP_Text nameText;
    private Item_Explanation _item_Ex;

    private void Start()
    {
        _item_Ex = Item_Explanation.Instance;
        nameText.text = NowSpell.SpellName;
    }

    public void SetData()
    {
        nameText.text = NowSpell.SpellName;
    }

    public void FireOnEnter()
    {
        _item_Ex.PointerOnEnter(NowSpell);
    }

    public void FireOnExit()
    {
        _item_Ex.PointerOnExit();
    }
}
