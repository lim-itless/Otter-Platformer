using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerView playerView;
    [SerializeField] private Rigidbody2D rigidbody;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;

    private void Update()
    {
        CheckGround();
        Move();
        Jump();
        Attack();
        Dance();
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        bool isMove = moveX != 0;

        playerView.SetMove(isMove);
        playerView.Flip(moveX);

        rigidbody.linearVelocity = new Vector2(moveX * moveSpeed, rigidbody.linearVelocity.y);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpForce);
            playerView.SetJump(true);
        }

        if (isGrounded && rigidbody.linearVelocity.y <= 0)
        {
            playerView.SetJump(false);
        }
    }
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            playerView.SetAttack(true);
            Invoke(nameof(EndAttack), 0.5f);
        }
    }

    private void Dance()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            playerView.SetDance(true);
            Invoke(nameof(EndDance), 0.5f);
        }
    }

    private void EndAttack()
    {
        playerView.SetAttack(false);
    }

    private void EndDance()
    {
        playerView.SetDance(false);
    }
}