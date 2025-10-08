using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using System.IO;

public class SheetDataDownloader : MonoBehaviour
{
    [SerializeField] private List<MonsterDataSO> monsterDataSO = new List<MonsterDataSO>();

    string folderPath = "Assets/Resources/Data/ScriptableObjects/Monsters";
    const string URL_MonsterDataSheet = "https://docs.google.com/spreadsheets/d/1zVeF4OT7lxJPJqBD75ok5iTlkjky9TK7xGys4N2dnYc/export?format=tsv&range=A1:F11";

    private void Awake()
    {
        StartDownload(false);
    }

    public void StartDownload(bool renameFiles)
    {
        StartCoroutine(DownloadMonsterData(renameFiles));
    }

    IEnumerator DownloadMonsterData(bool renameFiles)
    {
        UnityWebRequest www = UnityWebRequest.Get(URL_MonsterDataSheet);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string tsvText = www.downloadHandler.text;
            string json = ConvertTSVToJson(tsvText);

            JArray jsonData = JArray.Parse(json);
            ApplyDataToSO(jsonData, renameFiles);
        }
        else
        {
            Debug.LogError("데이터 가져오기 실패: " + www.error);
        }
    }

    string ConvertTSVToJson(string tsv)
    {
        string[] lines = tsv.Split('\n');
        if (lines.Length < 2) return "[]";

        string[] headers = lines[0].Split('\t');
        JArray jsonArray = new JArray();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split('\t');
            if (values.Length != headers.Length) continue;

            JObject jsonObject = new JObject();
            for (int j = 0; j < headers.Length; j++)
            {
                jsonObject[headers[j].Trim()] = values[j].Trim();
            }

            jsonArray.Add(jsonObject);
        }

        return jsonArray.ToString();
    }

    private void ApplyDataToSO(JArray jsonData, bool renameFiles)
    {
        ClearAllMonsterDataSO();
        monsterDataSO.Clear();

        foreach (JObject row in jsonData)
        {
            string tribe = row["종족"]?.ToString() ?? "";
            string type = row["속성"]?.ToString() ?? "";
            string health = row["체력"]?.ToString() ?? "";
            string attack = row["공격력"]?.ToString() ?? "";
            string damage = row["데미지"]?.ToString() ?? "";
            string effect = row["특수효과"]?.ToString() ?? "";

            MonsterDataSO monster = CreateNewMonsterDataSO(tribe);
            monster.SetData(tribe, type, health, attack, damage, effect);
            monsterDataSO.Add(monster);

            if (renameFiles)
                RenameScriptableObjectFile(monster, tribe);

            EditorUtility.SetDirty(monster);
            Debug.Log($"{monster.name} 업데이트 완료");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void ClearAllMonsterDataSO()
    {
        if (!Directory.Exists(folderPath)) return;

        string[] files = Directory.GetFiles(folderPath, "*.asset");
        foreach (string file in files)
            AssetDatabase.DeleteAsset(file);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private MonsterDataSO CreateNewMonsterDataSO(string fileName)
    {
        MonsterDataSO newSO = ScriptableObject.CreateInstance<MonsterDataSO>();
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string assetPath = $"{folderPath}/{fileName}.asset";
        AssetDatabase.CreateAsset(newSO, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        return newSO;
    }

    private void RenameScriptableObjectFile(MonsterDataSO so, string newFileName)
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(so);
        string newPath = Path.GetDirectoryName(path) + "/" + newFileName + ".asset";
        if (path != newPath)
        {
            AssetDatabase.RenameAsset(path, newFileName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
#endif
    }
}