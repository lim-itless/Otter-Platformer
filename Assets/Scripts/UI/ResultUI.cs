using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
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
        Debug.Log("Clear UI 표시");
        clearPanel.SetActive(true);
    }

    public void ShowGameOver()
    {
        Debug.Log("GameOver UI 표시");
        gameOverPanel.SetActive(true);
    }

    public void HidePanel()
    {
        clearPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }
}