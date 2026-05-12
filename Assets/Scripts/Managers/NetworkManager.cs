using System.IO;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Inst { get; private set; }

    private void Awake()
    {
        Inst = this;
    }

    private string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, "SaveData.json");
    }

    public void RequestSaveData(PlayerDataModel data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetPath(), json);
        Debug.Log($"저장 완료: {GetPath()}");
    }

    public PlayerDataModel RequestLoadData()
    {
        string path = GetPath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log("데이터 로드 성공");
            return JsonUtility.FromJson<PlayerDataModel>(json);
        }

        Debug.LogWarning("파일이 없어 새 데이터를 생성합니다.");
        return new PlayerDataModel();
    }
}