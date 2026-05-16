using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private Image loadedIconImage;

    [SerializeField] private TMP_Text shellCountText;
    [SerializeField] private TMP_Text scoreText;

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
        
        GameManager.Inst.OnShellCountChanged += UpdateShellCount;
        GameManager.Inst.OnScoreChanged += UpdateScore;

        UpdateShellCount(GameManager.Inst.PlayerData.CurrentShellCount);
        UpdateScore(GameManager.Inst.PlayerData.TotalScore);
    }

    private void OnDisable()
    {
        GameManager.Inst.OnShellCountChanged -= UpdateShellCount;
        GameManager.Inst.OnScoreChanged -= UpdateScore;
    }

    public void UpdateShellCount(int shellCount)
    {
        shellCountText.text = shellCount.ToString();
    }
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}