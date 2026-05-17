using UnityEngine;

public enum SpawnSpotType
{
    None = 0,
    Shell,
    Dialogue
}

public enum StartSpawnType
{
    None = 0,
    OnAwake,
    OnEnable,
    OnRange,
}

public class SpawnSpot : MonoBehaviour
{
    [SerializeField] private SpawnSpotType _spawnSpotType;
    [SerializeField] private StartSpawnType _startSpawnType;

    [SerializeField] private string _spawnObjectDataId;
    [SerializeField] private GameObject shellPrefab;
    [SerializeField] private Collider2D Collider_OnSpawnStart;
    [SerializeField] private Transform shellGroup;

    private void Awake()
    {
        if(_startSpawnType == StartSpawnType.OnAwake)
        {
            StartSpawn();
        }
    }

    private void OnEnable()
    {
        if (Collider_OnSpawnStart == null)
        {
            return;
        }

        Collider_OnSpawnStart.enabled = (_startSpawnType == StartSpawnType.OnRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == true)
        {
            StartSpawn();
        }
    }

    private void StartSpawn()
    {
        switch (_spawnSpotType)
        {
            case SpawnSpotType.Shell:
                if (shellPrefab == null)
                {
                    Debug.LogError($"{name} Shell Prefab 필요");
                    return;
                }

                if (shellGroup == null)
                {
                    Debug.LogError($"{name} Shell Group 필요");
                    return;
                }

                GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity, shellGroup);

                gameObject.SetActive(false);
                break;

            case SpawnSpotType.Dialogue:
                UIManager.Instance.OpenDialogueUI(_spawnObjectDataId);
                this.gameObject.SetActive(false);
                break;
        }
    }

    public void ResetSpawnSpot()
    {
        gameObject.SetActive(true);

        if (_spawnSpotType != SpawnSpotType.Shell)
        {
            return;
        }

        StartSpawn();
    }
}
