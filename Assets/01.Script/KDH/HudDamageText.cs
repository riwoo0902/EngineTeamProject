using TMPro;
using UnityEngine;

public class HudDamageText : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float alphaSpeed = 5;
    [SerializeField] private float destroyTime = 2;
    public TextMeshProUGUI text;
    Color alpha;

    public string damage;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = damage.ToString();
        alpha = text.color;
        Invoke("DestroyObject", destroyTime);
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
