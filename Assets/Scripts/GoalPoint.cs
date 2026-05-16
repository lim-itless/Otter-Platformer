using UnityEngine;

public class GoalPoint : MonoBehaviour
{
    private bool canClear;
    private bool isCleared;

    private void Update()
    {
        if (canClear == false)
        {
            return;
        }

        if (isCleared)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            isCleared = true;

            GameManager.Inst.ClearGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == false)
        {
            return;
        }

        canClear = true;

        Debug.Log("W키를 눌러 도착하기");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == false)
        {
            return;
        }

        canClear = false;
    }
}