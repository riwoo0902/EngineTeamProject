using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject first;
    [SerializeField] private GameObject secound;

    private bool isFirst = true;
    private void Start()
    {
        isFirst = true;
        first.SetActive(true);
        secound.SetActive(false);
    }
    public void Left()
    {
        if(isFirst)
        {
            first.SetActive(true);
            secound.SetActive(false);
            isFirst = false;
        }
    }

    public void Right()
    {
        if(!isFirst)
        {
            first.SetActive(false);
            secound.SetActive(true);
            isFirst = true;
        }
        else
        {
            SceneManager.LoadScene("Intro");
        }
    }
}
