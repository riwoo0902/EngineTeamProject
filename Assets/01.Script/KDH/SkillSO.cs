using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "SO/SkillSO")]
public class SkillSO : ScriptableObject
{
    public string skillName;
    public Sprite icon;
    public float cooldownTime;
    public GameObject effectPrefab;

    public void Execute(Transform caster)
    {
        // 여기서 개별 스킬 실행 
        if (effectPrefab != null)
        {
            GameObject obj = Instantiate(effectPrefab, caster.position, caster.rotation);
            Debug.Log($"{skillName} 발동");
        }
    }
}
