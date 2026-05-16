using UnityEngine;

public enum SpawnSpotType
{
    None = 0,
    Shell,
    DropItem,
    Dialogue,
    Monster
}

public enum StartSpawnType
{
    None = 0,
    OnAwake,
    OnEnable,
    OnRange,
    // UniTask나 코루틴으로 일정 시간마다 랜덤 생성도 구현해보자
}

public class SpawnSpot : MonoBehaviour
{
    [SerializeField] private SpawnSpotType _spawnSpotType;
    [SerializeField] private StartSpawnType _startSpawnType;

    [SerializeField] private string _spawnObjectDataId;
    [SerializeField] private GameObject shellPrefab;
    [SerializeField] private Collider2D Collider_OnSpawnStart;

    private void Awake()
    {
        if(_startSpawnType == StartSpawnType.OnAwake)
        {
            StartSpawn();
        }
    }

    private void Start()
    {
        if (_startSpawnType == StartSpawnType.OnEnable)
        {
            StartSpawn();
        }


        if (Collider_OnSpawnStart != null)
        {
            Collider_OnSpawnStart.enabled = (_startSpawnType == StartSpawnType.OnRange);
        }
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
                Instantiate(shellPrefab, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                break;
            case SpawnSpotType.DropItem:
                GameObjectManager.Inst.CreateFieldObject(_spawnObjectDataId, this.transform).Forget();
                this.gameObject.SetActive(false);
                break;
            case SpawnSpotType.Monster:
                break;
            case SpawnSpotType.Dialogue:
                UIManager.Instance.OpenDialogueUI(_spawnObjectDataId);
                this.gameObject.SetActive(false);
                break;
        }
    }

}
