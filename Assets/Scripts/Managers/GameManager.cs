using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
}