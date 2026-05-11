using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }

    private PlayerDataModel _playerModel = new PlayerDataModel();
    public PlayerDataModel PlayerData => _playerModel;

    private void Awake()
    {
        Inst = this;
    }

    private void Start()
    {
        _playerModel = NetworkManager.Inst.RequestLoadData();
    }

    public void AddShell(int amount)
    {
        _playerModel.CurrentShellCount += amount;
        _playerModel.TotalScore += (amount * 200);

        if (_playerModel.TotalScore > _playerModel.HighScore)
        {
            _playerModel.HighScore = _playerModel.TotalScore;
        }

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

}