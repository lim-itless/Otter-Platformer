using UnityEngine;

public class ShellItem : MonoBehaviour
{
    [SerializeField] private GameObject popupObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            popupObject.SetActive(true);

            Destroy(gameObject);
        }
    }
}