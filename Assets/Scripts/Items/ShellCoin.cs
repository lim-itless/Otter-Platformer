using UnityEngine;

public class ShellCoin : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 popupPosition = new Vector3(transform.position.x, transform.position.y + 0.7f, 0f);

            Instantiate(popupPrefab, popupPosition, Quaternion.identity);

            GameManager.Inst.AddShell(1);

            Destroy(gameObject);
        }
    }
}