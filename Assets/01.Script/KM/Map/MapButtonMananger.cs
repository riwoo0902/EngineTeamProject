using UnityEngine;

public class MapButtonMananger : MonoBehaviour
{
    static public MapButtonMananger Instance;
    public SaveStageData saveData;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void EnterMap()
    {
        saveData.ChoiceStage();
    }

}
