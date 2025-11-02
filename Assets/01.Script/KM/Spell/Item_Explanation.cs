using Lrw_PinBall;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Item_Explanation : MonoBehaviour
{
    public static Item_Explanation Instance { get; private set; }
    [SerializeField] private TMP_Text ItemName;
    [SerializeField] private TMP_Text Item_explanation;
    [SerializeField] private float spaceDistance = 5f;
    
    private string _itemName;
    private string _item_explanation;

    private RectTransform _rectCompo;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _rectCompo = GetComponent<RectTransform>();
        PointerOnExit();
    }

    public void PointerOnEnter(SpellDataSO item)
    {
        SettingSOinUi(item);
        _rectCompo.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _rectCompo.position = new Vector3(_rectCompo.position.x + spaceDistance, _rectCompo.position.y, 0);

    }

    public void PointerOnExit()
    {
        _rectCompo.position = new Vector3(10000, 10000, 0);
    }

    private void SettingSOinUi(SpellDataSO item)
    {
        _itemName = item.SpellName;
        _item_explanation = item.SpellDescription;

        ItemName.text = _itemName;
        Item_explanation.text = _item_explanation;
    }
}
