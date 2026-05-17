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
        titleUI.SetActive(true);
        mainHUD.SetActive(false);
        gameRoot.SetActive(false);

        resultUI.HidePanel();
    }

    public void StartGame()
    {
        titleUI.SetActive(false);
        mainHUD.SetActive(true);
        gameRoot.SetActive(true);
    }

    public void AddShell(int amount)
    {
        _playerModel.AddShell(amount, 200);

        Debug.Log($"조개: {_playerModel.CurrentShellCount}개 / 점수: {_playerModel.TotalScore}점");

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
        Debug.Log("게임 클리어!");
        OnGameClear?.Invoke();
    }

    public void GameOver()
    {
        Debug.Log("게임 오버!");

        OnGameOver?.Invoke();
    }

    public void RestartGame()
    {
        Debug.Log($"SpawnSpot 개수: {spawnSpots.Length}");

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

        playerRespawn.ResetPlayer();
        playerFallChecker.ResetFallState();
        goalPoint.ResetGoalPoint();
        resultUI.HidePanel();
    }
}