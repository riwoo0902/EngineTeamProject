using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public SkillSO skillData;
    [SerializeField] private Image icon;
    [SerializeField] private Image coolDownOverlay;

    private bool isCoolDown;
    private float currentCoolDown;

    private void Start()
    {
        icon.sprite = skillData.icon;
    }

    private void Update()
    {
        if (isCoolDown)
        {
            currentCoolDown -= Time.deltaTime;
            coolDownOverlay.fillAmount = currentCoolDown / skillData.cooldownTime;

            if (currentCoolDown <= 0)
            {
                isCoolDown = false;
                coolDownOverlay.fillAmount = 0;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isCoolDown && skillData != null)
        {
            UseSkill();
        }
    }

    private void UseSkill()
    {
        skillData.Execute(transform);
        StartCooldown();
    }

    private void StartCooldown()
    {
        isCoolDown = true;
        currentCoolDown = skillData.cooldownTime;
        coolDownOverlay.fillAmount = 1;
    }

}
