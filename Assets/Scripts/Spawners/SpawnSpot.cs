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

    private void Start()
    {
        if (Collider_OnSpawnStart != null)
        {
            Collider_OnSpawnStart.enabled = (_startSpawnType == StartSpawnType.OnRange);
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
                    Debug.LogError($"{name} Shell Prefab 없음");
                    return;
                }

                if (shellGroup == null)
                {
                    Debug.LogError($"{name} Shell Container 없음");
                    return;
                }

                GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity, shellGroup);

                Debug.Log($"{name} Shell 생성 완료 / 생성된 오브젝트: {shell.name} / 부모: {shell.transform.parent.name} / 컨테이너 자식 수: {shellGroup.childCount}");

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

    public void ResetSpawnSpot()
    {
        Debug.Log($"{name} ResetSpawnSpot 호출됨 / Type: {_spawnSpotType}");

        gameObject.SetActive(true);

        if (_spawnSpotType != SpawnSpotType.Shell)
        {
            return;
        }

        StartSpawn();
    }
}
