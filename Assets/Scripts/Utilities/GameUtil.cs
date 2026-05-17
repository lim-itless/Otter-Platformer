using Cysharp.Threading.Tasks;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public static class GameUtil
{
    public static void LoadFullData()
    {
        GameDataManager.Instance.LoadCharacterData("Character");
        GameDataManager.Instance.LoadDialogueData();
    }

    //public static string GetFullDataPath(string dataTableName)
    //{
    //    if (string.IsNullOrEmpty(dataTableName))
    //    {
    //        Debug.Log("테이블 이름이 올바르지 않습니다!");
    //        return string.Empty;
    //    }

    //    string relativePath = $"JsonConverter/JsonOutput/{dataTableName}.json";
    //    string fullPath = Path.GetFullPath(relativePath);
    //    return fullPath;
    //}

    public static Sprite LoadSpriteCanBeNull(string spriteName)
    {
        Sprite loadedSprite = Resources.Load<Sprite>($"{spriteName}");

        if (loadedSprite != null)
        {
            return loadedSprite;
        }

        Debug.LogError($"에셋을 찾을 수 없습니다: {spriteName}");
        return null;
    }

    public static async UniTaskVoid LoadAndSetTexture(RawImage targetRawImage, string texturePath)
    {
        targetRawImage.gameObject.SetActive(false);
        Texture texture = await ResourceManager.Inst.LoadAsset<Texture>(texturePath);
        if (texture != null)
        {
            targetRawImage.texture = texture;
        }
        targetRawImage.gameObject.SetActive(true);
    }
}