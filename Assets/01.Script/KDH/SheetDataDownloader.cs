using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using System.IO;

public class SheetDataDownloader : MonoBehaviour
{
    // TSV : 탭으로 구분된 값들
    // JSON : 자바스크립트 객체 형태로 데이터를 표현한 형식

    // 생성된 MonsterDataSO들을 저장해 둘 리스트
    [SerializeField] private List<MonsterDataSO> monsterDataSO = new List<MonsterDataSO>();

    // 몬스터 SO가 저장될 폴더 경로 (에셋 생성 위치)
    string folderPath = "Assets/Resources/Data/ScriptableObjects/Monsters";
    // 구글 스프레드시트에서 TSV 형식으로 몬스터 데이터를 가져오는 URL
    const string URL_MonsterDataSheet = "https://docs.google.com/spreadsheets/d/1zVeF4OT7lxJPJqBD75ok5iTlkjky9TK7xGys4N2dnYc/export?format=tsv&range=A1:F11";

    private void Awake()
    {
        // 시작하자마자 시트 다운로드 코루틴 실행
        StartDownload(false);
    }

    // 외부에서 호출해서 시트 다운로드를 시작하는 함수
    public void StartDownload(bool renameFiles)
    {
        // 몬스터 데이터 다운로드 코루틴 시작
        StartCoroutine(DownloadMonsterData(renameFiles));
    }

    // 구글 시트에서 몬스터 데이터를 받아오는 코루틴
    IEnumerator DownloadMonsterData(bool renameFiles)
    {
        // 설정해둔 URL로 GET 요청 생성
        UnityWebRequest www = UnityWebRequest.Get(URL_MonsterDataSheet); // HTTP : 다양한 구조의 데이터를 전송받을 수 있음
        // 요청이 끝날 때까지 기다림 
        yield return www.SendWebRequest();

        // 요청이 성공했는지 확인
        if (www.result == UnityWebRequest.Result.Success)
        {
            // TSV 텍스트 데이터(탭으로 구분된 텍스트)를 문자열로 가져옴
            string tsvText = www.downloadHandler.text;
            // TSV 형식을 JSON 문자열로 변환
            string json = ConvertTSVToJson(tsvText);

            // JSON 문자열을 JArray 형태로 파싱 (줄 단위 데이터 집합)
            JArray jsonData = JArray.Parse(json);
            // 파싱한 데이터를 ScriptableObject에 반영 (생성/갱신)
            ApplyDataToSO(jsonData, renameFiles);
        }
        else
        {
            Debug.LogError("데이터 가져오기 실패: " + www.error);
        }
    }

    // TSV(탭으로 구분된 값) 문자열을 JSON 배열 문자열로 바꾸는 함수
    string ConvertTSVToJson(string tsv)
    {
        // 줄 기준으로 문자열 나누기
        string[] lines = tsv.Split('\n');

        // 최소 2줄(헤더 + 데이터)은 있어야 함, 아니면 빈 배열 반환
        if (lines.Length < 2) return "[]";

        // 첫 줄은 헤더(컬럼 이름)들이라 탭으로 쪼갬
        string[] headers = lines[0].Split('\t');

        // 최종 결과를 담을 JSON 배열
        JArray jsonArray = new JArray();

        // 두 번째 줄부터 실제 데이터
        for (int i = 1; i < lines.Length; i++)
        {
            // 한 줄을 탭 기준으로 값들로 분리
            string[] values = lines[i].Split('\t');

            // 헤더 개수랑 값 개수가 다르면 잘못된 줄이므로 스킵
            if (values.Length != headers.Length) continue;

            // 한 줄을 표현할 JSON 객체 생성
            JObject jsonObject = new JObject();

            // 각 헤더 이름을 key, 해당 값을 value로 매칭
            for (int j = 0; j < headers.Length; j++)
            {
                // 양쪽 공백 제거해서 깔끔하게 저장
                jsonObject[headers[j].Trim()] = values[j].Trim();
            }

            // 완성된 한 줄 데이터를 JSON 배열에 추가
            jsonArray.Add(jsonObject);
        }

        // JSON 배열을 문자열로 변환해서 반환
        return jsonArray.ToString();
    }

    // JSON 데이터를 실제 MonsterDataSO 에셋으로 만들어서 반영하는 함수
    private void ApplyDataToSO(JArray jsonData, bool renameFiles)
    {
        // 기존에 있던 모든 몬스터 SO 에셋 제거
        ClearAllMonsterDataSO();

        // 리스트도 초기화
        monsterDataSO.Clear();

        // JSON 배열을 한 줄씩(JObject) 순회
        foreach (JObject row in jsonData)
        {
            // 각 컬럼 이름으로 값 꺼내기 (없으면 빈 문자열)
            string tribe = row["종족"]?.ToString() ?? ""; // 종족 이름
            string type = row["속성"]?.ToString() ?? ""; // 속성
            string health = row["체력"]?.ToString() ?? ""; // 체력
            string attack = row["공격력"]?.ToString() ?? ""; // 공격력
            string damage = row["데미지"]?.ToString() ?? ""; // 데미지 값
            string effect = row["특수효과"]?.ToString() ?? ""; // 특수 효과 설명

            // 종족 이름을 파일 이름으로 하는 새 MonsterDataSO 에셋 생성
            MonsterDataSO monster = CreateNewMonsterDataSO(tribe);

            // SO 안에 실제 데이터 세팅 (SetData는 MonsterDataSO 안에 정의된 함수라고 가정)
            monster.SetData(tribe, type, health, attack, damage, effect);

            // 리스트에 방금 만든 SO 추가 (나중에 디버그나 확인용)
            monsterDataSO.Add(monster);

            // 파일 이름을 따로 바꾸고 싶으면 실행 (renameFiles 옵션)
            if (renameFiles)
                RenameScriptableObjectFile(monster, tribe);

            // 해당 SO가 수정되었다고 에디터에 알림 (저장 가능 상태로 표시)
            EditorUtility.SetDirty(monster);

            // 디버그 로그 출력
            Debug.Log($"{monster.name} 업데이트 완료");
        }

        // 모든 변경 사항을 디스크에 저장
        AssetDatabase.SaveAssets();

        // 프로젝트 뷰 갱신
        AssetDatabase.Refresh();
    }

    // 폴더 내에 있는 모든 MonsterDataSO 에셋(.asset 파일) 삭제
    private void ClearAllMonsterDataSO()
    {
        // 폴더가 없으면 할 게 없음
        if (!Directory.Exists(folderPath)) return;

        // 해당 폴더 안의 .asset 파일 경로들 전부 가져오기
        string[] files = Directory.GetFiles(folderPath, "*.asset");

        // 각각 AssetDatabase를 통해 삭제
        foreach (string file in files)
            AssetDatabase.DeleteAsset(file);

        // 삭제 후 에셋 저장
        AssetDatabase.SaveAssets();

        // 프로젝트 뷰 갱신
        AssetDatabase.Refresh();
    }

    // 새로운 MonsterDataSO 에셋을 생성하고 반환하는 함수
    private MonsterDataSO CreateNewMonsterDataSO(string fileName)
    {
        // 메모리상에서 MonsterDataSO 인스턴스 생성
        MonsterDataSO newSO = ScriptableObject.CreateInstance<MonsterDataSO>();

        // 폴더가 없으면 디렉토리 생성
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        // 생성할 에셋의 풀 경로 설정
        string assetPath = $"{folderPath}/{fileName}.asset";

        // 실제 에셋 파일 생성 (newSO를 assetPath 위치에 저장)
        AssetDatabase.CreateAsset(newSO, assetPath);

        // 변경 사항 저장
        AssetDatabase.SaveAssets();

        // 프로젝트 뷰 갱신
        AssetDatabase.Refresh();

        // 만든 SO 반환
        return newSO;
    }

    // 이미 존재하는 ScriptableObject 에셋의 파일 이름을 바꾸는 함수
    private void RenameScriptableObjectFile(MonsterDataSO so, string newFileName)
    {
#if UNITY_EDITOR
        // 해당 ScriptableObject가 디스크 상에서 어떤 경로에 있는지 얻기
        string path = AssetDatabase.GetAssetPath(so);

        // 같은 폴더 + 새로운 파일명으로 경로 구성
        string newPath = Path.GetDirectoryName(path) + "/" + newFileName + ".asset";

        // 경로가 다를 때만 이름 변경 시도
        if (path != newPath)
        {
            // 실제 에셋 이름 변경
            AssetDatabase.RenameAsset(path, newFileName);

            // 변경 사항 저장
            AssetDatabase.SaveAssets();

            // 프로젝트 뷰 갱신
            AssetDatabase.Refresh();
        }
#endif
    }
}