using TMPro;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text highScoreText;

    private void Start()
    {
        if (GameManager.Inst == null)
        {
            return;
        }

        clearPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        GameManager.Inst.OnGameClear += ShowClear;
        GameManager.Inst.OnGameOver += ShowGameOver;
    }

    private void OnDisable()
    {
        if (GameManager.Inst != null)
        {
            GameManager.Inst.OnGameClear -= ShowClear;
            GameManager.Inst.OnGameOver -= ShowGameOver;
        }
    }

    private void ShowClear()
    {
        clearPanel.SetActive(true);

        int highScore = GameManager.Inst.PlayerData.HighScore;

        highScoreText.text = $"최고 기록 : {highScore}";
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void HidePanel()
    {
        clearPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

}