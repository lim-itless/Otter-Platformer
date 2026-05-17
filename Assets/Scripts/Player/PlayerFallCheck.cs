using UnityEngine;

public class PlayerFallChecker : MonoBehaviour
{
    [SerializeField] private float fallLimitY = -10f;

    private bool isGameOver;

    private void Update()
    {
        if (isGameOver)
        {
            return;
        }

        if (transform.position.y < fallLimitY)
        {
            isGameOver = true;

            GameManager.Inst.GameOver();
        }
    }

    public void ResetFallState()
    {
        isGameOver = false;
    }
}