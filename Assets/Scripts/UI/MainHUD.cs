using UnityEngine;
using UnityEngine.UI;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private Image loadedIconImage;

    private void Start()
    {
        loadedIconImage.enabled = false;

        Sprite loadedSprite = Resources.Load<Sprite>("Icon/Shell");

        if (loadedSprite == null)
        {
            Debug.LogError("리소스 로드 실패");
            return;
        }

        loadedIconImage.sprite = loadedSprite;
        loadedIconImage.enabled = true;
    }
}