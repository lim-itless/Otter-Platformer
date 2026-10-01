using UnityEngine;

public class ShellCoin : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private AudioClip pickupSfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Inst == null)
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            Vector3 popupPosition = new Vector3(transform.position.x, transform.position.y + 0.7f, 0f);

            if (popupPrefab != null)
            {
                Instantiate(popupPrefab, popupPosition, Quaternion.identity);
            }

            if (SoundManager.Inst != null)
            {
                SoundManager.Inst.PlaySFX(pickupSfx);
            }

            GameManager.Inst.AddShell(1);

            Destroy(gameObject);
        }
    }
}