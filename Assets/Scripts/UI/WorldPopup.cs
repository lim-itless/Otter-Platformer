using System.Collections;
using UnityEngine;

public class WorldPopup : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(CoCloseSelf());
    }

    private IEnumerator CoCloseSelf()
    {
        yield return new WaitForSeconds(3f);

        Destroy(gameObject);
    }
}