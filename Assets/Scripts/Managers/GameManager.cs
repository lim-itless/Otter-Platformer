using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameRoot;
    [SerializeField] private GameObject mainHUD;
    [SerializeField] private GameObject titleUI;
    [SerializeField] private PlayerRespawn playerRespawn;
    [SerializeField] private PlayerFallChecker playerFallChecker;
    [SerializeField] private GoalPoint goalPoint;
    [SerializeField] private SpawnSpot[] spawnSpots;
    [SerializeField] private Transform shellGroup;
    [SerializeField] private ResultUI resultUI;
    public static GameManager Inst { get; private set; }

    public event Action<int> OnShellCountChanged;
    public event Action<int> OnScoreChanged;
    public event Action OnGameClear;
    public event Action OnGameOver;

    private PlayerDataModel _playerModel = new PlayerDataModel();

    public PlayerDataModel PlayerData => _playerModel;

    private void Awake()
    {
        Inst = this;
    }

    private void Start()
    {
        _playerModel = NetworkManager.Inst.RequestLoadData();
        _playerModel.ResetRunData();

        ShowTitle();
    }

    public void ShowTitle()
    {
        if (titleUI != null)
        {
            titleUI.SetActive(true);
        }
        if (mainHUD != null)
        {
            mainHUD.SetActive(false);
        }
        if (gameRoot != null)
        {
            gameRoot.SetActive(false);
        }
        if (resultUI != null)
        {
            resultUI.HidePanel();
        }
    }

    public void StartGame()
    {
        if (titleUI != null)
        {
            titleUI.SetActive(false);
        }
        if (mainHUD != null)
        {
            mainHUD.SetActive(true);
        }
        if (gameRoot != null)
        {
            gameRoot.SetActive(true);
        }
    }

    public void AddShell(int amount)
    {
        _playerModel.AddShell(amount, 200);

        OnShellCountChanged?.Invoke(_playerModel.CurrentShellCount);
        OnScoreChanged?.Invoke(_playerModel.TotalScore);

        if (_playerModel.CurrentShellCount % 10 == 0)
        {
            SaveData();
        }
    }

    public void SaveData()
    {
        NetworkManager.Inst.RequestSaveData(_playerModel);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void ClearGame()
    {
        OnGameClear?.Invoke();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public void RestartGame()
    {
        _playerModel.ResetRunData();

        OnShellCountChanged?.Invoke(_playerModel.CurrentShellCount);
        OnScoreChanged?.Invoke(_playerModel.TotalScore);

        StartGame();

        if (shellGroup != null)
        {
            for (int i = shellGroup.childCount - 1; i >= 0; i--)
            {
                Destroy(shellGroup.GetChild(i).gameObject);
            }
        }

        if (spawnSpots != null)
        {
            for (int i = 0; i < spawnSpots.Length; i++)
            {
                if (spawnSpots[i] == null)
                {
                    continue;
                }

                spawnSpots[i].ResetSpawnSpot();
            }
        }

        if (playerRespawn != null)
        {
            playerRespawn.ResetPlayer();
        }

        if (playerFallChecker != null)
        {
            playerFallChecker.ResetFallState();
        }

        if (goalPoint != null)
        {
            goalPoint.ResetGoalPoint();
        }

        if (resultUI != null)
        {
            resultUI.HidePanel();
        }
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}