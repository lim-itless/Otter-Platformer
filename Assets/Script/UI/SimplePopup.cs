using System.Collections;
using UnityEngine;

public class SimplePopup : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(CoCloseSelf());
    }

    private IEnumerator CoCloseSelf()
    {
        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);
    }
}