using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void SetMove(bool isMove)
    {
        animator.SetBool("IsMove", isMove);
    }

    public void SetAttack(bool isAttack)
    {
        animator.SetBool("IsAttack", isAttack);
    }

    public void Flip(float moveX)
    {
        if (moveX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveX < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}