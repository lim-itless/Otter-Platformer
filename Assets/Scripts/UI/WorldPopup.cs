using System.Collections;
using UnityEngine;

public class WorldPopup : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;

    private void OnEnable()
    {
        StartCoroutine(CoCloseSelf());
    }

    private IEnumerator CoCloseSelf()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}