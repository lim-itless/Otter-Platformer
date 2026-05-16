using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private GameObject clearPanel;

    private void Start()
    {
        clearPanel.SetActive(false);

        GameManager.Inst.OnGameClear += ShowClear;
    }

    private void OnDisable()
    {
        if (GameManager.Inst != null)
        {
            GameManager.Inst.OnGameClear -= ShowClear;
        }
    }

    private void ShowClear()
    {
        clearPanel.SetActive(true);
    }
}