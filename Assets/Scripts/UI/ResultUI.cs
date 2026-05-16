using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
        clearPanel.SetActive(false);

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
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}