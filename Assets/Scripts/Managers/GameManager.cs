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
        _playerModel.AddShell(amount, 200);

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