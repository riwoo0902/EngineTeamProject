using DG.Tweening;
using TMPro;
using UnityEngine;

public class AttackEffact : MonoBehaviour
{
    public void AttackDamage(int damage)
    {
        TextMeshPro text = gameObject.GetComponent<TextMeshPro>();
        text.text = damage.ToString();
        gameObject.transform.DOMoveY(transform.position.y + 3, 1.3f);
        text.DOFade(0, 1.3f).OnComplete(() => EndEffack());
    }

    public void EndEffack()
    {
        Destroy(gameObject);
    }
}
