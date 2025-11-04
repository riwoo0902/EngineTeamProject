using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour, IPointerClickHandler
{
    public SkillSO skillData;
    [SerializeField] private Image icon;
    [SerializeField] private Image coolDownOverlay;

    private bool isCoolDown;
    private float currentCoolDown;

    private void Start()
    {
        if (skillData == null)
        {
            return;
        }

        icon.sprite = skillData.icon;
    }

    private void Update()
    {
        if (isCoolDown)
        {
            currentCoolDown -= Time.deltaTime;
            coolDownOverlay.fillAmount = 1 - (currentCoolDown / skillData.cooldownTime);

            if (currentCoolDown <= 0)
            {
                isCoolDown = false;
                coolDownOverlay.fillAmount = 0;
                Debug.Log($"ÄðÅ¸ÀÓ Á¾·á");
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (skillData == null)
        {
            return;
        }

        if (!isCoolDown)
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